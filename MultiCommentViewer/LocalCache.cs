using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCommentViewer
{
    using Microsoft.Web.WebView2.Core;
    // 必要な using
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

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
    public static class LocalCache
    {
        // exe フォルダの Cookies サブフォルダを返す
        public static string GetCacheFolders()
        {
            var exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location)
             ?? AppDomain.CurrentDomain.BaseDirectory;
            var folder = System.IO.Path.Combine(exeDir, "LocalCache");
            Directory.CreateDirectory(folder);
            return folder;
        }

        // 保存先パス（サイト名で分ける）
        public static string GetCachePath(string siteName)
        {
            var safeName = string.IsNullOrWhiteSpace(siteName) ? "default" : MakeFileSystemSafe(siteName);
            return Path.Combine(GetCacheFolders(), safeName + ".bin");
        }

        // ファイル名に使える安全文字列に変換（簡易）
        private static string MakeFileSystemSafe(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }

        // CoreWebView2 の Cookie リストを DTO に変換して暗号化してファイルに保存
        public static async Task WriteAsync(IEnumerable<CoreWebView2Cookie> cookies, string siteName)
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
            var path = GetCachePath(siteName);
            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
