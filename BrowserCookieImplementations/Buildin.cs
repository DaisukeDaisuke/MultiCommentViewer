using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ryu_s.BrowserCookie
{
    public class BuildinManager : IBrowserManager
    {
        public BrowserType Type => BrowserType.Buildin; // 新しいBrowserTypeが必要

        public List<IBrowserProfile> GetProfiles()
        {
            var profiles = new List<IBrowserProfile>();
            profiles.Add(new BuildinProfile("default"));
            return profiles;
        }

        private string GetCookieFolder()
        {
            var exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                         ?? AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(exeDir, "Cookies");
        }
    }

    public class BuildinProfile : IBrowserProfile
    {
        public string Path { get; }
        public string ProfileName { get; }
        public BrowserType Type => BrowserType.Buildin;

        public BuildinProfile(string profileName)
        {
            ProfileName = profileName ?? "default";
            Path = GetCookieFilePath(ProfileName);
        }

        public Cookie GetCookie(string domain, string name)
        {
            var cookies = GetCookieCollection(domain);
            return cookies.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Cookie> GetCookieCollection(string domain)
        {

            Console.WriteLine($"Loading cookies for domain: {domain} from {Path}");

            string fileName = domain switch
            {
                "mirrativ.com" => "Mirrativ.bin",
                "www.mirrativ.com" => "Mirrativ.bin",
                null => "unknown",
                _ => "unknown" // これがあると安全です
            };

            var allCookies = LoadAllCookies(fileName);
            var result = new List<Cookie>();

            foreach (var cookie in allCookies) {
                // ドメインマッチングロジック
                if (IsHostMatch(cookie.Domain, domain))
                {
                    result.Add(cookie);
                }
                Debug.WriteLine(cookie.Domain);
            }
            return result;
        }

        private List<Cookie> LoadAllCookies(string file)
        {
            var cookieList = new List<Cookie>();

            var path1 = System.IO.Path.Combine(Path, file);

            if (!File.Exists(path1))
            {
                return cookieList;
            }

            try
            {
                // 暗号化されたファイルを読み込み
                var encrypted = File.ReadAllBytes(path1);

                // DPAPI で復号
                var bytes = ProtectedData.Unprotect(encrypted, optionalEntropy: null, scope: DataProtectionScope.LocalMachine);
                var json = Encoding.UTF8.GetString(bytes);

                // JSON を CookieDto のリストにデシリアライズ
                var cookieDtos = JsonSerializer.Deserialize<List<CookieDto>>(json);

                if (cookieDtos != null)
                {
                    foreach (var dto in cookieDtos)
                    {
                        try
                        {
                            var cookie = new Cookie(dto.Name, dto.Value, dto.Path ?? "/", dto.Domain ?? "");

                            // 有効期限を設定
                            if (dto.Expires.HasValue)
                            {
                                cookie.Expires = dto.Expires.Value;
                            }

                            // その他のプロパティを設定
                            cookie.HttpOnly = dto.IsHttpOnly;
                            cookie.Secure = dto.IsSecure;

                            cookieList.Add(cookie);
                        }
                        catch (Exception)
                        {
                            // 無効なクッキーは無視して続行
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 復号化エラーまたはJSONパースエラーの場合は空のリストを返す
            }

            return cookieList;
        }

        private bool IsHostMatch(string cookieDomain, string requestDomain)
        {
            if (string.IsNullOrEmpty(cookieDomain) || string.IsNullOrEmpty(requestDomain))
            {
                return false;
            }

            // 完全一致
            if (cookieDomain.Equals(requestDomain, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // ドット付きドメイン（.example.com）の場合、サブドメインマッチング
            if (cookieDomain.StartsWith("."))
            {
                var domain = cookieDomain.Substring(1);
                return requestDomain.Equals(domain, StringComparison.OrdinalIgnoreCase) ||
                       requestDomain.EndsWith("." + domain, StringComparison.OrdinalIgnoreCase);
            }

            // 逆パターン：requestDomainがサブドメインの場合
            if (requestDomain.Contains(cookieDomain))
            {
                return requestDomain.EndsWith("." + cookieDomain, StringComparison.OrdinalIgnoreCase) ||
                       requestDomain.Equals(cookieDomain, StringComparison.OrdinalIgnoreCase);
            }

            if (!cookieDomain.StartsWith(".") && cookieDomain.EndsWith(requestDomain))
            {
                return true;
            }

            return false;
        }

        private string GetCookieFilePath(string siteName)
        {
            var exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                         ?? AppDomain.CurrentDomain.BaseDirectory;
            var folder = System.IO.Path.Combine(exeDir, "Cookies");
            Directory.CreateDirectory(folder);
            return folder;
        }
    }

    // CookieDto クラス（CookieStorage.cs から移動または重複定義）
    public class CookieDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime? Expires { get; set; } // null = session cookie
        public bool IsHttpOnly { get; set; }
        public bool IsSecure { get; set; }
    }
}