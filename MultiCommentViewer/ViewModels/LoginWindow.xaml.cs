using Microsoft.Web.WebView2.Core;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MultiCommentViewer.ViewModels
{
    /// <summary>
    /// LoginWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string _siteName;
        private TaskCompletionSource<LoginResult> _completionSource;

        public LoginWindow(string siteName, string loginUrl)
        {
            InitializeComponent();
            _siteName = siteName;
            _completionSource = new TaskCompletionSource<LoginResult>();
            InitializeWebView(loginUrl);
        }

        // 非同期でログイン結果を待つためのメソッド
        public Task<LoginResult> WaitForCompletionAsync()
        {
            return _completionSource.Task;
        }

        private async void InitializeWebView(string loginUrl)
        {
            try
            {
                // exe のあるフォルダを取得
                var exeDir = System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location
                );

                // exe の場所に "Browser" フォルダを作成
                var _userDataFolder = System.IO.Path.Combine(
                    exeDir,
                    "Browser",
                    "default"
                );

                // フォルダがなければ作成
                System.IO.Directory.CreateDirectory(_userDataFolder);

                WebView.CoreWebView2InitializationCompleted += (s, e) =>
                {
                    if (e.IsSuccess)
                    {
                        WebView.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);

                        WebView.CoreWebView2.WebResourceRequested += (s, e) =>
                        {
                            var headers = e.Request.Headers;

                            // Sec-CH-UA が存在する場合のみ上書き
                            if (headers.Contains("user-agent"))
                            {
                                headers.SetHeader("user-agent",
                                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0");
                            }

                            if (headers.Contains("Sec-CH-UA"))
                            {
                                headers.SetHeader("Sec-CH-UA",
                                    "\"Chromium\";v=\"140\", \"Not=A?Brand\";v=\"24\", \"Microsoft Edge\";v=\"140\"");
                            }

                            if (headers.Contains("Sec-CH-UA-Full-Version"))
                            {
                                headers.SetHeader("Sec-CH-UA-Full-Version", "\"140.0.7339.81\"");
                            }

                            if (headers.Contains("Sec-CH-UA-Full-Version-List"))
                            {
                                headers.SetHeader("Sec-CH-UA-Full-Version-List",
                                    "\"Chromium\";v=\"140.0.7339.81\", \"Not=A?Brand\";v=\"24.0.0.0\", \"Microsoft Edge\";v=\"140.0.7339.81\"");
                            }

                            if (headers.Contains("Sec-CH-UA-Platform"))
                            {
                                headers.SetHeader("Sec-CH-UA-Platform", "\"Windows\"");
                            }

                            if (headers.Contains("Sec-CH-UA-Platform-Version"))
                            {
                                headers.SetHeader("Sec-CH-UA-Platform-Version", "\"15.0.0\"");
                            }
                            if (headers.Contains("uaFullVersion"))
                            {
                                headers.SetHeader("uaFullVersion", "\"139.0.3405.125\"");
                            }
                        };

                        string spoofScript = @"
                    (() => {
                        const spoofed = {
                            brands: [
                                { brand: 'Chromium', version: '140' },
                                { brand: 'Not=A?Brand', version: '24' },
                                { brand: 'Microsoft Edge', version: '140' }
                            ],
                            getHighEntropyValues: async (hints) => {
                                return {
                                    brands: [
                                        { brand: 'Chromium', version: '140' },
                                        { brand: 'Not=A?Brand', version: '24' },
                                        { brand: 'Microsoft Edge', version: '140' }
                                    ],
                                    fullVersionList: [
                                        { brand: 'Chromium', version: '140.0.7339.81' },
                                        { brand: 'Not=A?Brand', version: '24.0.0.0' },
                                        { brand: 'Microsoft Edge', version: '140.0.7339.81' }
                                    ],
                                    platform: 'Windows',
                                    platformVersion: '15.0.0',
                                    architecture: 'x86',
                                    bitness: '64',
                                    model: '',
                                    wow64: false,
                                    formFactors: ['Desktop'],
                                    uaFullVersion: '140.0.3485.54',
                                    mobile: false,
                                }
                            }
                        };

                        Object.defineProperty(navigator, 'userAgentData', {
                            value: spoofed,
                            configurable: true
                        });
                    })();
                ";

                        WebView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(spoofScript);
                        WebView.CoreWebView2.Navigate(loginUrl);
                    }
                    else
                    {
                        var result = new LoginResult
                        {
                            IsCompleted = false,
                            ErrorMessage = $"WebView2 初期化失敗: {e.InitializationException.Message}"
                        };
                        _completionSource.SetResult(result);
                        Close();
                    }
                };

                var env = await CoreWebView2Environment.CreateAsync(userDataFolder: _userDataFolder);
                await WebView.EnsureCoreWebView2Async(env);
            }
            catch (Exception ex)
            {
                var result = new LoginResult
                {
                    IsCompleted = false,
                    ErrorMessage = $"WebView2の初期化に失敗しました: {ex.Message}"
                };
                _completionSource.SetResult(result);
                Close();
            }
        }

        private async void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cookieManager = WebView.CoreWebView2.CookieManager;
                var cookies = await cookieManager.GetCookiesAsync(null);

                // 保存（暗号化）
                await CookieStorage.SaveCookiesEncryptedAsync(cookies, _siteName);

                // Cookie 文字列を既存のロジックと互換させたい場合は組み替え
                var cookieString = string.Join("; ", System.Linq.Enumerable.Select(cookies, c => $"{c.Name}={c.Value}"));

                var result = new LoginResult
                {
                    IsCompleted = true,
                    Cookies = cookieString
                };

                _completionSource.SetResult(result);
                Close();
            }
            catch (Exception ex)
            {
                var result = new LoginResult
                {
                    IsCompleted = false,
                    ErrorMessage = $"クッキーの取得に失敗しました: {ex.Message}"
                };
                _completionSource.SetResult(result);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var result = new LoginResult
            {
                IsCompleted = false,
                ErrorMessage = "ユーザーによりキャンセルされました"
            };
            _completionSource.SetResult(result);
            Close();
        }

        // ウィンドウが閉じられた時の処理
        protected override void OnClosed(EventArgs e)
        {
            // まだ完了していない場合はキャンセル扱いにする
            if (!_completionSource.Task.IsCompleted)
            {
                var result = new LoginResult
                {
                    IsCompleted = false,
                    ErrorMessage = "ウィンドウが閉じられました"
                };
                _completionSource.SetResult(result);
            }
            base.OnClosed(e);
        }
    }

    // ログイン結果を格納するクラス
    public class LoginResult
    {
        public bool IsCompleted { get; set; }
        public string Cookies { get; set; }
        public string ErrorMessage { get; set; }
    }
}