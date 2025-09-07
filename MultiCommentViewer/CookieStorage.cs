using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCommentViewer
{
    using Microsoft.Web.WebView2.Core;
    using Newtonsoft.Json;
    // 必要な using
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Security;

    // Cookie DTO（保存フォーマット）
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

    // ユーティリティ／保存ロジック
    public static class CookieStorage
    {
        // exe フォルダの Cookies サブフォルダを返す
        public static string GetCookieFolder()
        {
            var exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                         ?? AppDomain.CurrentDomain.BaseDirectory;
            var folder = Path.Combine(exeDir, "Cookies");
            Directory.CreateDirectory(folder);
            return folder;
        }

        // 保存先パス（サイト名で分ける）
        public static string GetCookieFilePath(string siteName)
        {
            var safeName = string.IsNullOrWhiteSpace(siteName) ? "default" : MakeFileSystemSafe(siteName);
            return Path.Combine(GetCookieFolder(), safeName + ".bin");
        }

        // ファイル名に使える安全文字列に変換（簡易）
        private static string MakeFileSystemSafe(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }

        // CoreWebView2 の Cookie リストを DTO に変換して暗号化してファイルに保存
        public static async Task SaveCookiesEncryptedAsync(IEnumerable<CoreWebView2Cookie> cookies, string siteName)
        {
            var list = new List<CookieDto>();
            foreach (var c in cookies)
            {
                // 修正版1: より安全なキャスト方法
                DateTime? expires = null;
                if (c.Expires != null)
                {
                    // c.Expiresの実際の型に応じて適切にキャスト
                    if (c.Expires is DateTime dt)
                    {
                        expires = dt;
                    }
                    else
                    {
                        // 文字列や他の型の場合はパースを試行
                        if (DateTime.TryParse(c.Expires.ToString(), out var parsed))
                        {
                            expires = parsed;
                        }
                    }
                }

                list.Add(new CookieDto
                {
                    Name = c.Name,
                    Value = c.Value,
                    Domain = c.Domain,
                    Path = c.Path,
                    Expires = expires,
                    IsHttpOnly = c.IsHttpOnly,
                    IsSecure = c.IsSecure
                });
            }

            var json = System.Text.Json.JsonSerializer.Serialize(list);
            var bytes = Encoding.UTF8.GetBytes(json);

            // DPAPI を使って暗号化（LocalMachine スコープ：同一マシン上の別プロセスでも復号可能）
            var encrypted = ProtectedData.Protect(bytes, optionalEntropy: null, scope: DataProtectionScope.LocalMachine);

            var path = GetCookieFilePath(siteName);
            File.WriteAllBytes(path, encrypted);
        }

        // 暗号化ファイルを復号して CookieContainer を返す
        public static CookieContainer LoadCookiesFromEncryptedFile(string siteName)
        {
            var path = GetCookieFilePath(siteName);
            if (!File.Exists(path)) return new CookieContainer();

            var encrypted = File.ReadAllBytes(path);

            // 復号
            var bytes = ProtectedData.Unprotect(encrypted, optionalEntropy: null, scope: DataProtectionScope.LocalMachine);
            var json = Encoding.UTF8.GetString(bytes);

            var list = System.Text.Json.JsonSerializer.Deserialize<List<CookieDto>>(json);
            var container = new CookieContainer();

            if (list != null)
            {
                foreach (var dto in list)
                {
                    try
                    {
                        var cookie = new Cookie(dto.Name, dto.Value, dto.Path ?? "/", dto.Domain ?? "");
                        if (dto.Expires.HasValue) cookie.Expires = dto.Expires.Value;
                        cookie.HttpOnly = dto.IsHttpOnly;
                        cookie.Secure = dto.IsSecure;

                        // CookieContainer.Add のドメイン取り扱いに注意：
                        // domain は先頭にドットが必要な場合があるためそのまま渡す
                        container.Add(cookie);
                    }
                    catch
                    {
                        // 無効な cookie があれば無視して続行
                    }
                }
            }

            return container;
        }
    }
}