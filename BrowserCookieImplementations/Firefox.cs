using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace ryu_s.BrowserCookie
{
    using ryu_s.BrowserCookie.Firefox;
    public class FirefoxManager : IFirefoxManager
    {
        #region IFirefoxManager
        public BrowserType Type { get; }

        public List<IBrowserProfile> GetProfiles()
        {
            var profileFileName = "profiles.ini";
            var list = new List<IBrowserProfile>();
            if (!File.Exists(Path.Combine(_moz_path, profileFileName)))
            {
                //多分Firefoxをインストールしていない
                return list;
            }

            var profiles = FirefoxProfile.GetProfiles(_moz_path, profileFileName);
            foreach (var profile in profiles)
            {
                if (profile.IsDefault)
                    list.Insert(0, new FirefoxCookie(profile));
                else
                    list.Add(new FirefoxCookie(profile));
            }
            return list;
        }
        public FirefoxManager()
        {
            Type = BrowserType.Firefox;
        }
        #endregion

        class FirefoxCookie : IBrowserProfile
        {
            #region IBrowserProfile
            public string Path { get; }

            public string ProfileName { get; }

            public BrowserType Type { get; }

            public Cookie GetCookie(string domain, string name)
            {
                // originAttributes ごとに同一 (name, host, path) が複数存在しうるため
                // 通常コンテナ (originAttributes = '') のものを優先して返す
                var query = "SELECT value, name, host, path, expiry, isSecure, isHttpOnly, originAttributes " +
                            "FROM moz_cookies " +
                            "WHERE host LIKE '%" + domain + "' AND name = '" + name + "' " +
                            "ORDER BY CASE WHEN originAttributes = '' THEN 0 ELSE 1 END";
                var collection = GetCookieCollectionInternal(query);
                return (collection != null && collection.Count > 0) ? collection[0] : null;
            }

            public List<Cookie> GetCookieCollection(string domain)
            {
                var query = "SELECT value, name, host, path, expiry, isSecure, isHttpOnly, originAttributes " +
                            "FROM moz_cookies " +
                            "WHERE host LIKE '%" + domain + "'";
                return GetCookieCollectionInternal(query);
            }
            #endregion

            #region Methods
            /// <summary>
            /// Firefox はWALモード(Write-Ahead Logging)で動作しているため、
            /// ロック中でも .sqlite-wal / .sqlite-shm ファイルを含めてコピーする。
            /// </summary>
            private List<Cookie> GetCookieCollectionInternal(string query)
            {
                var tempFile = new TempFileProvider();
                var srcPath = Path;

                // メインDBをコピー
                System.IO.File.Copy(srcPath, tempFile.Path, true);

                // WALモード対応: -wal / -shm が存在すれば同じ一時ディレクトリへコピーする。
                // これらが揃っていないと未コミットの最新データが読めない場合がある。
                var walSrc = srcPath + "-wal";
                var shmSrc = srcPath + "-shm";
                var walDst = tempFile.Path + "-wal";
                var shmDst = tempFile.Path + "-shm";

                if (System.IO.File.Exists(walSrc))
                    System.IO.File.Copy(walSrc, walDst, true);
                if (System.IO.File.Exists(shmSrc))
                    System.IO.File.Copy(shmSrc, shmDst, true);

                var list = new List<Cookie>();
                System.Data.DataTable dt = null;
                try
                {
                    using (var conn = SQLiteHelper.CreateConnection(tempFile.Path))
                    {
                        conn.Open();
                        dt = SQLiteHelper.ExecuteReader(conn, query);
                    }
                }
                finally
                {
                    // 一時ファイルを確実に削除
                    TryDeleteFile(tempFile.Path);
                    TryDeleteFile(walDst);
                    TryDeleteFile(shmDst);
                }

                if (dt != null)
                {
                    var cc = new CookieContainer();
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        // Firefox 104+ スキーマ:
                        // id, originAttributes, name, value, host, path,
                        // expiry, lastAccessed, creationTime,
                        // isSecure, isHttpOnly, inBrowserElement(廃止→sameSite),
                        // sameSite, rawSameSite, schemeMap
                        var value = row["value"].ToString();
                        var name = row["name"].ToString();
                        var host = row["host"].ToString();
                        var path = row["path"].ToString();
                        var expires = long.Parse(row["expiry"].ToString());
                        var isSecure = row["isSecure"] != DBNull.Value && Convert.ToInt32(row["isSecure"]) != 0;
                        var isHttpOnly = row["isHttpOnly"] != DBNull.Value && Convert.ToInt32(row["isHttpOnly"]) != 0;

                        // expiry = 0 はセッションクッキー (DateTime.MinValue = セッション扱い)。
                        //
                        // Firefox の expiry カラムは仕様上 Unix 秒だが、
                        // 実際には一部のクッキーでミリ秒が入っているケースが存在する。
                        // 判定基準: Unix 秒の上限 (year 9999 = 253402300799) を超えていれば
                        // ミリ秒と見なして 1000 で割る。
                        // それでも上限を超える場合(遠い未来の巨大値)はクランプする。
                        const long MaxUnixSeconds = 253402300799L;
                        DateTime expiresDate;
                        if (expires <= 0)
                        {
                            expiresDate = DateTime.MinValue; // セッションクッキー
                        }
                        else
                        {
                            long expirySeconds = (expires > MaxUnixSeconds)
                                ? expires / 1000   // ミリ秒→秒に変換
                                : expires;
                            // 変換後も上限を超える場合(例: マイクロ秒で入っていた等)はクランプ
                            expirySeconds = Math.Min(expirySeconds, MaxUnixSeconds);
                            expiresDate = Tools.FromUnixTime(expirySeconds);
                        }

                        var cookie = new Cookie(name, value, path, host)
                        {
                            Expires = expiresDate,
                            Secure = isSecure,
                            HttpOnly = isHttpOnly,
                        };

                        try
                        {
                            // CookieContainer に適合しない大きな value は CookieException になるため除外する。
                            // expiry 以外のプロパティ起因の ArgumentOutOfRangeException も念のため除外する。
                            cc.Add(cookie);
                            list.Add(cookie);
                        }
                        catch (CookieException) { }
                        catch (ArgumentOutOfRangeException) { }
                    }
                }
                return list;
            }

            private static void TryDeleteFile(string path)
            {
                try
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
                catch { /* 削除失敗はサイレントに無視 */ }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="source"></param>
            /// <returns></returns>
            private string UnProtect(byte[] source)
            {
                return NativeMethods.CryptUnprotectData(source, Encoding.UTF8);
            }
            #endregion

            public FirefoxCookie(FirefoxProfile profile)
            {
                Path = profile.path + "\\" + _cookieFilename;
                ProfileName = profile.Name;
                Type = BrowserType.Firefox;
            }

            private readonly string _cookieFilename = "cookies.sqlite";
        }

        private readonly string _moz_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox";
    }
}

namespace ryu_s.BrowserCookie.Firefox
{
    /// <summary>
    /// Firefox プロファイル情報。
    /// Firefox 67 以降は installs.ini でインストールごとにデフォルトプロファイルが管理される。
    /// profiles.ini の Default=1 だけでなく、installs.ini の Default キーも参照して
    /// IsDefault を設定する。
    /// </summary>
    internal class FirefoxProfile
    {
        public string Name { get; private set; }
        public bool IsRelative { get; private set; } = false;
        public bool IsDefault { get; private set; } = false;
        public string path = "";

        /// <summary>
        /// 
        /// </summary>
        public static FirefoxProfile GetDefaultProfile(string moz_path, string iniFileName)
        {
            var profiles = GetProfiles(moz_path, iniFileName);
            if (profiles.Count == 1)
                return profiles[0];
            foreach (var profile in profiles)
            {
                if (profile.IsDefault)
                    return profile;
            }
            return null;
        }

        /// <summary>
        /// profiles.ini を読み込みつつ、installs.ini が存在すれば
        /// その Default エントリと照合して IsDefault を補正する。
        /// </summary>
        public static List<FirefoxProfile> GetProfiles(string moz_path, string iniFileName)
        {
            var path = moz_path + "\\" + iniFileName;
            if (!System.IO.File.Exists(path))
                throw new FirefoxProfileIniNotFoundException($"path={path}");

            List<FirefoxProfile> list;
            using (var sr = new System.IO.StreamReader(path, Encoding.UTF8))
            {
                list = GetProfiles(ReadLines(sr), moz_path);
            }

            // Firefox 67+ の installs.ini を読み、現インストールのデフォルトプロファイルを特定する
            ApplyInstallsIni(list, moz_path);

            return list;
        }

        /// <summary>
        /// installs.ini から Default パスを読み取り、一致するプロファイルの IsDefault を true にする。
        /// installs.ini が存在しない場合や解析できない場合は何もしない(既存の IsDefault をそのまま使う)。
        /// </summary>
        private static void ApplyInstallsIni(List<FirefoxProfile> profiles, string moz_path)
        {
            var installsIniPath = moz_path + "\\installs.ini";
            if (!System.IO.File.Exists(installsIniPath))
                return;

            // installs.ini の各セクションから Default キーを収集する
            var defaultPaths = new List<string>();
            try
            {
                using (var sr = new System.IO.StreamReader(installsIniPath, Encoding.UTF8))
                {
                    string currentDefault = null;
                    bool inSection = false;
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine()?.Trim();
                        if (string.IsNullOrEmpty(line)) continue;

                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            if (currentDefault != null)
                                defaultPaths.Add(currentDefault);
                            currentDefault = null;
                            inSection = true;
                            continue;
                        }
                        if (!inSection) continue;

                        var pair = FirefoxProfile.SplitByEqual(line);
                        if (pair.Key == "Default")
                        {
                            // IsRelative なパスを絶対パスへ変換
                            var p = pair.Value.Replace("/", "\\");
                            if (!System.IO.Path.IsPathRooted(p))
                                p = moz_path + "\\" + p;
                            currentDefault = p;
                        }
                    }
                    if (currentDefault != null)
                        defaultPaths.Add(currentDefault);
                }
            }
            catch
            {
                return; // 読み込み失敗時は既存の IsDefault に従う
            }

            if (defaultPaths.Count == 0)
                return;

            // defaultPaths に一致するプロファイルを IsDefault=true にする
            // (profiles.ini の Default=1 だけでなく installs.ini ベースでも上書き)
            bool anyMatched = false;
            foreach (var profile in profiles)
            {
                var normalizedProfilePath = System.IO.Path.GetFullPath(profile.path).TrimEnd('\\');
                foreach (var dp in defaultPaths)
                {
                    var normalizedDefault = System.IO.Path.GetFullPath(dp).TrimEnd('\\');
                    if (string.Equals(normalizedProfilePath, normalizedDefault, StringComparison.OrdinalIgnoreCase))
                    {
                        profile.IsDefault = true;
                        anyMatched = true;
                        break;
                    }
                }
            }

            // installs.ini に一致するものが見つかれば、profiles.ini 由来の Default=1 は上書きしない
            // (anyMatched=false の場合は既存の IsDefault をそのまま維持)
        }

        public static List<FirefoxProfile> GetProfiles(IEnumerable<string> lines, string moz_path)
        {
            var list = new List<FirefoxProfile>();
            FirefoxProfile profile = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("[Profile"))
                {
                    profile = new FirefoxProfile();
                    list.Add(profile);
                    continue;
                }
                if (profile != null)
                {
                    var pair = SplitByEqual(line);
                    switch (pair.Key)
                    {
                        case "Name":
                            profile.Name = pair.Value;
                            break;
                        case "IsRelative":
                            profile.IsRelative = (pair.Value == "1");
                            break;
                        case "Path":
                            profile.path = pair.Value.Replace("/", "\\");
                            if (profile.IsRelative)
                                profile.path = moz_path + "\\" + profile.path;
                            break;
                        case "Default":
                            profile.IsDefault = (pair.Value == "1");
                            break;
                        default:
                            break;
                    }
                }
            }
            return list;
        }

        public static IEnumerable<string> ReadLines(System.IO.StreamReader sr)
        {
            while (!sr.EndOfStream)
            {
                yield return sr.ReadLine();
            }
        }

        /// <summary>
        /// 文字列を'='で2つに分割する。
        /// </summary>
        internal static KeyValuePair<string, string> SplitByEqual(string line)
        {
            var arr = line.Split('=').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            if (arr.Length >= 2)
            {
                var pos = line.IndexOf('=');
                return new KeyValuePair<string, string>(arr[0], line.Substring(pos + 1));
            }
            else
            {
                return new KeyValuePair<string, string>();
            }
        }
    }
}
