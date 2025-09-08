using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MultiCommentViewer.ViewModels
{
    public partial class LoginWindow : Window
    {
        private string _siteName;
        private TaskCompletionSource<LoginResult> _completionSource;
        private List<WebView2> _webViews;
        private TabControl _tabControl;

        public LoginWindow(string siteName, string loginUrl)
        {
            InitializeComponent();
            _siteName = siteName;
            _completionSource = new TaskCompletionSource<LoginResult>();
            _webViews = new List<WebView2>();

            InitializeTabControl();
            AddNewBrowserTab("メイン", loginUrl);
        }

        // タブコントロールを初期化
        private void InitializeTabControl()
        {
            _tabControl = new TabControl();

            var grid = (Grid)this.Content;
            grid.Children.Add(_tabControl);
            Grid.SetRow(_tabControl, 1);
            _tabControl.Margin = new Thickness(10);
        }

        // 新しいブラウザタブを追加
        public void AddNewBrowserTab(string tabName, string url)
        {
            var tabItem = new TabItem
            {
                Header = tabName
            };

            var webView = CreateWebView2(url);
            if (webView != null)
            {
                tabItem.Content = webView;

                _tabControl.Items.Add(tabItem);
                _tabControl.SelectedItem = tabItem;
                _webViews.Add(webView);
            }
        }

        // WebView2を動的に作成
        private WebView2 CreateWebView2(string loginUrl)
        {
            try
            {
                var webView = new WebView2();

                // exe のあるフォルダを取得
                var exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location)
                             ?? AppDomain.CurrentDomain.BaseDirectory;

                var userDataFolder = System.IO.Path.Combine(
                    exeDir,
                    "Browser",
                    $"profile"
                );

                System.IO.Directory.CreateDirectory(userDataFolder);

                webView.CreationProperties = new CoreWebView2CreationProperties
                {
                    BrowserExecutableFolder = "dll/WebView2",
                    UserDataFolder = userDataFolder
                };

                // 初期化完了時の処理
                webView.CoreWebView2InitializationCompleted += async (s, e) =>
                {
                    if (e.IsSuccess)
                    {
                        await SetupWebView(webView, loginUrl);
                    }
                    else
                    {
                        MessageBox.Show($"WebView2 初期化失敗: {e.InitializationException.Message}");
                    }
                };

                // 非同期で初期化を開始
                _ = Task.Run(async () =>
                {
                    await Dispatcher.InvokeAsync(async () =>
                    {
                        await webView.EnsureCoreWebView2Async();
                    });
                });
                return webView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return null;
        }

        // WebViewの設定
        private async Task SetupWebView(WebView2 webView, string loginUrl)
        {
            webView.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);

            webView.CoreWebView2.WebResourceRequested += (s, e) =>
            {
                var headers = e.Request.Headers;

                // User-Agent関連のヘッダーを設定
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

            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(spoofScript);
            webView.CoreWebView2.Navigate(loginUrl);
        }

        //// 新しいタブを追加するメソッド（外部から呼び出し可能）
        //public void AddBrowserTab(string siteName, string url)
        //{
        //    AddNewBrowserTab(siteName, url);
        //}

        //// タブを閉じるメソッド
        //public void CloseBrowserTab(int index)
        //{
        //    if (index >= 0 && index < _tabControl.Items.Count)
        //    {
        //        var tabItem = (TabItem)_tabControl.Items[index];
        //        var webView = (WebView2)tabItem.Content;

        //        _webViews.Remove(webView);
        //        _tabControl.Items.RemoveAt(index);

        //        // WebView2のリソースを解放
        //        webView?.Dispose();
        //    }
        //}

        //// 現在アクティブなWebViewを取得
        //public WebView2 GetActiveWebView()
        //{
        //    if (_tabControl.SelectedItem is TabItem selectedTab)
        //    {
        //        return selectedTab.Content as WebView2;
        //    }
        //    return null;
        //}

        // 完了ボタンクリック時 - 全てのWebViewからクッキーを取得
        private async void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var allCookies = new List<CoreWebView2Cookie>();

                foreach (var webView in _webViews)
                {
                    if (webView.CoreWebView2 != null)
                    {
                        var cookieManager = webView.CoreWebView2.CookieManager;
                        var cookies = await cookieManager.GetCookiesAsync(null);
                        allCookies.AddRange(cookies);
                    }
                }

                LocalCache.WriteCache(allCookies, _siteName);

                var result = new LoginResult
                {
                    IsCompleted = true
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

        // 非同期でログイン結果を待つためのメソッド
        public Task<LoginResult> WaitForCompletionAsync()
        {
            return _completionSource.Task;
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

        protected override void OnClosed(EventArgs e)
        {
            // WebView2のリソースを解放
            foreach (var webView in _webViews)
            {
                webView?.Dispose();
            }

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

    public class LoginResult
    {
        public bool IsCompleted { get; set; }
        public string ErrorMessage { get; set; }
    }
}