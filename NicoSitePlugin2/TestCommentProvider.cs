using Common;
using ryu_s.BrowserCookie;
using SitePlugin;
using SitePluginCommon;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using NicoSitePlugin.Metadata;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using NicoSitePlugin2.Client;
using Dwango.Nicolive.Chat.Service.Edge;
using System.Linq;
using System.Web.UI.WebControls;
using static Dwango.Nicolive.Chat.Data.Enquete.Types;

namespace NicoSitePlugin
{
    class TestCommentProvider : CommentProviderBase, INicoCommentProvider, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IUserStoreManager _userStoreManager;
        private readonly INicoSiteOptions _siteOptions;
        private readonly IDataSource _server;
        private readonly Metadata.MetaProvider _metaProvider;
        CancellationTokenSource _disconnectCts;
        private DataProps ExtractDataProps(string livePagehtml)
        {
            var match = Regex.Match(livePagehtml, "<script [^>]+ data-props=\"([^>]+)\"></script>");
            if (!match.Success) return null;
            var pre = match.Groups[1].Value;
            var dataPropsJson = pre.Replace("&quot;", "\"");
            var dataProps = new DataProps(dataPropsJson);
            return dataProps;
        }
        public override async Task ConnectAsync(string input, IBrowserProfile browserProfile)
        {
            BeforeConnect();
            var nicoInput = Tools.ParseInput(input);
            if (nicoInput is InvalidInput invalidInput)
            {
                SendSystemInfo("未対応の形式のURLが入力されました", InfoType.Error);
                AfterDisconnected();
                return;
            }
            _isFirstConnection = true;
reload:
            _isDisconnectedExpected = false;
            _disconnectCts = new CancellationTokenSource();
            try
            {
                await ConnectInternalAsync(nicoInput, browserProfile);
            }
            catch (ApiGetCommunityLivesException ex)
            {
                _isDisconnectedExpected = true;
                SendSystemInfo("コミュニティの配信状況の取得に失敗しました", InfoType.Error);
                _logger.LogException(ex, "", $"input:{input}, browser:{browserProfile.Type}");
            }
            catch (SpecChangedException ex)
            {
                _isDisconnectedExpected = true;
                SendSystemInfo("サイトの仕様変更があったためコメント取得を継続できません", InfoType.Error);
                _logger.LogException(ex, "", $"input:{input}, browser:{browserProfile.Type}");
            }
            catch (Exception ex)
            {
                _isDisconnectedExpected = true;
                SendSystemInfo("オフライン、またはニコ生視聴ページにアクセスできません。", InfoType.Error);
                _logger.LogException(ex, "", $"input:{input}, browser:{browserProfile.Type}");
            }
            _dataProps = null;
            if (!_isDisconnectedExpected)
            {
                _isFirstConnection = false;
                goto reload;
            }
            var m = new NicoDisconnected("");
            var s = new DisconnectedMessageMetadata(m, _options, _siteOptions);
            var c = new NicoMessageContext(m, s, new NicoMessageMethods());
            RaiseMessageReceived(c);
            AfterDisconnected();
        }
        private CookieContainer GetCookieContainer(IBrowserProfile browserProfile)
        {
            return GetCookieContainer(browserProfile, "nicovideo.jp");
        }
        private async Task<string> GetChannelLiveId(ChannelUrl channelUrl)
        {
check:
            var currentLiveId = await Api.GetCurrentChannelLiveId(_server, channelUrl.ChannelScreenName);
            if (currentLiveId != null)
            {
                return currentLiveId;
            }
            else
            {
                RaiseMetadataUpdated(new TestMetadata
                {
                    Title = "（次の配信が始まるまで待機中...）",
                });
                await Task.Delay(30 * 1000, _disconnectCts.Token);
                goto check;
            }
        }
        private async Task<string> GetUserLiveId(UserId UserId, CookieContainer cc)
        {
    check:
            var currentLiveId = await Api.GetUserIdToCurrentLiveId(_server, UserId.Raw, cc);
            if (currentLiveId != null)
            {
                return currentLiveId;
            }
            else
            {
                RaiseMetadataUpdated(new TestMetadata
                {
                    Title = "（次の配信が始まるまで待機中...）",
                });
                await Task.Delay(30 * 1000, _disconnectCts.Token);
                goto check;
            }
        }
        private async Task<string> GetCommunityLiveId(CommunityUrl communityUrl, CookieContainer cc)
        {
check:
            var currentLiveId = await Api.GetCurrentCommunityLiveId(_server, communityUrl.CommunityId, cc);
            if (currentLiveId != null)
            {
                return currentLiveId;
            }
            else
            {
                RaiseMetadataUpdated(new TestMetadata
                {
                    Title = "（次の配信が始まるまで待機中...）",
                });
                await Task.Delay(30 * 1000, _disconnectCts.Token);
                goto check;
            }
        }
        CookieContainer _cc;
        public async Task ConnectInternalAsync(IInput input, IBrowserProfile browserProfile)
        {
            var cc = GetCookieContainer(browserProfile);
            _cc = cc;
            string vid;
            if (input is LivePageUrl livePageUrl)
            {
                vid = livePageUrl.LiveId;
            }
            else if (input is ChannelUrl channelUrl)
            {
                vid = await GetChannelLiveId(channelUrl);
            }
            else if (input is CommunityUrl communityUrl)
            {
                vid = await GetCommunityLiveId(communityUrl, cc);
            }
            else if (input is LiveId liveId)
            {
                vid = liveId.Raw;
            }
            else if (input is UserId UserId)
            {
                vid = await GetUserLiveId(UserId, cc);
            }
            else
            {
                throw new InvalidOperationException("bug");
            }
            var url = "https://live2.nicovideo.jp/watch/" + vid;


            var liveHtml = await _server.GetAsync(url, cc);
            _dataProps = ExtractDataProps(liveHtml);
            if (_dataProps == null)
            {
                throw new SpecChangedException("data-propsが無い", liveHtml);
            }
            if (_dataProps.Status == "ENDED")
            {
                SendSystemInfo("この番組は終了しました", InfoType.Notice);
                if (input is LivePageUrl)//チャンネルやコミュニティのURLを入力した場合は次の配信が始まるまで待機する
                {
                    _isDisconnectedExpected = true;
                }
                return;
            }

            _vposBaseTime = Common.UnixTimeConverter.FromUnixTime(_dataProps.VposBaseTime);
            _localTime = DateTime.Now;
            RaiseMetadataUpdated(new TestMetadata
            {
                Title = _dataProps.Title,
            });


            var metaTask = _metaProvider.ReceiveAsync(_dataProps.WebsocketUrl);
            _tasks.Add(metaTask);
            _mainLooptcs = new TaskCompletionSource<object>();
            _tasks.Add(_mainLooptcs.Task);

            while (_tasks.Count > 1)//1の場合は_mainLooptcs.Taskだからループを終了する
            {
                var t = await Task.WhenAny(_tasks);
                if (t == _mainLooptcs.Task)
                {
                    _tasks.Remove(_mainLooptcs.Task);
                    _tasks.AddRange(_toAdd);
                    _toAdd.Clear();
                    _mainLooptcs = new TaskCompletionSource<object>();
                    _tasks.Add(_mainLooptcs.Task);
                }
                else if (t == metaTask)
                {
                    try
                    {
                        await metaTask;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogException(ex);
                    }
                    _tasks.Remove(metaTask);
                }
                else//roomTask
                {
                    _metaProvider?.Disconnect();
                    try
                    {
                        await metaTask;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogException(ex);
                    }
                    _tasks.Remove(metaTask);
                    try
                    {
                        await t;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogException(ex);
                    }
                    _tasks.Clear();//本当はchatのTaskだけ取り除きたいけど、変数に取ってなくて無理だから全部消しちゃう
                }
            }
            return;
        }
        /// <summary>
        /// 初期コメント取得中か
        /// </summary>
        private bool _isInitialCommentsReceiving;
        protected readonly ConcurrentDictionary<string, int> _userCommentCountDict = new ConcurrentDictionary<string, int>();
        /// <summary>
        /// 意図的な切断か
        /// </summary>
        private bool _isDisconnectedExpected;
        /// <summary>
        /// 一番最初の接続か。再接続時はfalse。
        /// 再接続時は初期コメントが不要だから主にその判別に使うフラグ
        /// </summary>
        private bool _isFirstConnection;
        private static bool IsAd(Chat.ChatMessage chat)
        {
            return chat.Content.StartsWith("/nicoad ");
        }
        private static bool IsGift(Chat.ChatMessage chat)
        {
            return chat.Content.StartsWith("/gift ");
        }
        private static bool IsSpi(Chat.ChatMessage chat)
        {
            return chat.Content.StartsWith("/spi ");
        }
        private static bool IsEmotion(Chat.ChatMessage chat)
        {
            return chat.Content.StartsWith("/emotion ");
        }
        private static bool IsInfo(Chat.ChatMessage chat)
        {
            return chat.Content.StartsWith("/info ");
        }
        private static bool IsDisconnect(Chat.ChatMessage chat)
        {
            return chat.Content == "/disconnect";
        }
        /// <summary>
        /// 生IDか
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool IsRawUserId(string userId)
        {
            return !string.IsNullOrEmpty(userId) && Regex.IsMatch(userId, "^\\d+$");
        }
        private Task<string> GetUserName(string userId)
        {
            throw new NotImplementedException();
        }
        private const string SystemUserId = "900000000";
        private static string? GetThumbnail(string userId)
        {
            if (long.TryParse(userId, out var userIdNum))
            {
                var k = userIdNum / 10000;
                return $"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/{k}/{userId}.jpg";
            }
            return null;
        }
        private async Task ProcessChatMessageAsync(Chat.IChatMessage message)
        {
            switch (message)
            {
                case Chat.ChatMessage chat:
                    {
                        if (_isFirstConnection == false && _isInitialCommentsReceiving == true)
                        {
                            //再接続時は初期コメントを無視する
                            return;
                        }
                        var userId = chat.UserId;
                        var user = GetUser(userId);
                        bool isFirstComment;
                        if (_userCommentCountDict.ContainsKey(userId))
                        {
                            _userCommentCountDict[userId]++;
                            isFirstComment = false;
                        }
                        else
                        {
                            _userCommentCountDict.AddOrUpdate(userId, 1, (s, n) => n);
                            isFirstComment = true;
                        }
                        var thumbNailUrl = GetThumbnail(userId);
                        //var comment = await Tools.CreateNicoComment(chat, user, _siteOptions, roomName, async userid => await API.GetUserInfo(_dataSource, userid), _logger);
                        INicoMessage comment;
                        INicoMessageMetadata metadata;
                        if (IsAd(chat))
                        {
                            ///nicoad {"totalAdPoint":215500,"message":"シュガーさんが1700ptニコニ広告しました","version":"1"}
                            var adJson = chat.Content.Replace("/nicoad", "");
                            dynamic d = JsonConvert.DeserializeObject(adJson);
                            if ((string)d.version != "1")
                            {
                                throw new ParseException(chat.Raw);
                            }
                            var content = (string)d.message;
                            var ad = new NicoAd(chat.Raw)
                            {
                                PostedAt = Common.UnixTimeConverter.FromUnixTime(chat.Date),
                                UserId = userId,
                                Text = content,
                            };
                            comment = ad;
                            metadata = new AdMessageMetadata(ad, _options, _siteOptions)
                            {
                                IsInitialComment = _isInitialCommentsReceiving,
                                SiteContextGuid = SiteContextGuid,
                            };
                        }
                        else if (IsGift(chat))
                        {
                            var match = Regex.Match(chat.Content, "/gift (\\S+) (\\d+|NULL) \"(\\S+)\" (\\d+) \"(\\S*)\" \"(\\S+)\"(?: (\\d+))?");
                            if (!match.Success)
                            {
                                throw new ParseException(chat.Raw);
                            }
                            var giftId = match.Groups[1].Value;
                            var userIdp = match.Groups[2].Value;//ギフトを投げた人。userId == "900000000"
                            var username = match.Groups[3].Value;
                            var point = match.Groups[4].Value;
                            var what = match.Groups[5].Value;
                            var itemName = match.Groups[6].Value;
                            var itemCount = match.Groups[7].Value;//アイテムの個数？ギフト貢献n位？
                            var text = $"{username}さんがギフト「{itemName}（{point}pt）」を贈りました";
                            var gift = new NicoGift(chat.Raw)
                            {
                                Text = text,
                                PostedAt = Common.UnixTimeConverter.FromUnixTime(chat.Date),
                                UserId = userIdp == "NULL" ? "" : userIdp,
                                NameItems = Common.MessagePartFactory.CreateMessageItems(username),
                            };
                            comment = gift;
                            metadata = new ItemMessageMetadata(gift, _options, _siteOptions)
                            {
                                IsInitialComment = _isInitialCommentsReceiving,
                                SiteContextGuid = SiteContextGuid,
                            };
                        }
                        else if (IsSpi(chat))
                        {
                            var spi = new NicoSpi(chat.Raw)
                            {
                                Text = chat.Content,
                                PostedAt = Common.UnixTimeConverter.FromUnixTime(chat.Date),
                                UserId = chat.UserId,
                            };
                            comment = spi;
                            metadata = new SpiMessageMetadata(spi, _options, _siteOptions)
                            {
                                IsInitialComment = _isInitialCommentsReceiving,
                                SiteContextGuid = SiteContextGuid,
                            };
                        }
                        else if (IsEmotion(chat))
                        {
                            var content = chat.Content.Substring("/emotion ".Length);
                            var abc = new NicoEmotion("")
                            {
                                ChatNo = chat.No,
                                Anonymity = chat.Anonymity,
                                PostedAt = Common.UnixTimeConverter.FromUnixTime(chat.Date),
                                Content = content,
                                UserId = chat.UserId,
                            };
                            comment = abc;
                            metadata = new EmotionMessageMetadata(abc, _options, _siteOptions, user, this)
                            {
                                IsInitialComment = _isInitialCommentsReceiving,
                                SiteContextGuid = SiteContextGuid,
                            };
                        }
                        else if (IsInfo(chat))
                        {
                            var match = Regex.Match(chat.Content, "^/info (?<no>\\d+) (?<content>.+)$", RegexOptions.Singleline);
                            if (!match.Success)
                            {
                                throw new ParseException(chat.Raw);
                            }
                            else
                            {
                                var no = int.Parse(match.Groups["no"].Value);
                                var content = match.Groups["content"].Value;
                                var info = new NicoInfo(chat.Raw)
                                {
                                    Text = content,
                                    PostedAt = Common.UnixTimeConverter.FromUnixTime(chat.Date),
                                    UserId = chat.UserId,
                                    No = no,
                                };
                                comment = info;
                                metadata = new InfoMessageMetadata(info, _options, _siteOptions)
                                {
                                    IsInitialComment = _isInitialCommentsReceiving,
                                    SiteContextGuid = SiteContextGuid,
                                };
                            }
                        }
                        else
                        {
                            if (IsDisconnect(chat))//NicoCommentではなく専用のクラスを作っても良いかも。
                            {
                                _chatProvider?.Disconnect();
                            }
                            if (_siteOptions.IsAutoSetNickname)
                            {
                                var nick = SitePluginCommon.Utils.ExtractNickname(chat.Content);
                                if (!string.IsNullOrEmpty(nick))
                                {
                                    user.Nickname = nick;
                                }
                            }
                            var abc = new NicoComment("")
                            {
                                ChatNo = chat.No,
                                Id = chat.No.ToString(),
                                Is184 = chat.Anonymity == 1,
                                PostedAt = Common.UnixTimeConverter.FromUnixTime(chat.Date),
                                Text = chat.Content,
                                UserId = chat.UserId,
                                UserName = chat.Name,
                                ThumbnailUrl = thumbNailUrl,
                            };
                            comment = abc;
                            metadata = new CommentMessageMetadata(abc, _options, _siteOptions, user, this, isFirstComment)
                            {
                                IsInitialComment = _isInitialCommentsReceiving,
                                SiteContextGuid = SiteContextGuid,
                            };
                        }


                        var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                        RaiseMessageReceived(context);
                    }
                    break;
                case Chat.Ping ping:
                    if (ping.Content == "rs:0")
                    {
                        _isInitialCommentsReceiving = true;
                    }
                    else if (ping.Content == "rf:0")
                    {
                        _isInitialCommentsReceiving = false;
                    }
                    break;
                case Chat.UnknownMessage unknown:
                    _logger.LogException(new ParseException(unknown.Raw));
                    break;
                default:
                    break;
            }
        }
        private async void ChatProvider_Received(object sender, Chat.IChatMessage e)
        {
            var message = e;
            try
            {
                await ProcessChatMessageAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

        }

        readonly List<Task> _tasks = new List<Task>();
        readonly List<Task> _toAdd = new List<Task>();
        TaskCompletionSource<object> _mainLooptcs;
        private readonly Chat.ChatProvider _chatProvider;
        Metadata.Room _room;
        DataProps _dataProps;
        private bool _disposedValue;
        private MessageServerClient? _messageServerClient = null;

        private void MetaProvider_Received(object sender, Metadata.IMetaMessage e)
        {
            var message = e;

            try
            {
                switch (message)
                {
                    case Metadata.Room room:
                        {
                            _room = room;
                            Chat.IChatOptions chatOptions;
                            if (Metadata.Room.IsLoggedIn(room))
                            {
                                chatOptions = new Chat.ChatLoggedInOptions
                                {
                                    Thread = room.ThreadId,
                                    ThreadKey = room.YourPostKey,
                                    UserId = _dataProps.UserId,
                                };
                            }
                            else
                            {
                                chatOptions = new Chat.ChatGuestOptions
                                {
                                    Thread = room.ThreadId,
                                    UserId = "guest",
                                };
                            }
                            var t = _chatProvider.ReceiveAsync(chatOptions);
                            _toAdd.Add(t);
                            _mainLooptcs.SetResult(null);
                        }
                        break;
                    case Metadata.Ping ping:
                        _metaProvider?.Send(new Metadata.Pong());
                        break;
                    case Metadata.Statistics stat:
                        RaiseMetadataUpdated(new TestMetadata
                        {
                            TotalViewers = stat.Viewers.ToString(),
                            Others = $"コメント数:{stat.Comments} 広告ポイント:{stat.AdPoints} ギフトポイント:{stat.GiftPoints}",
                        });
                        break;
                    case Metadata.Disconnect disconnect:
                        if (disconnect.Reason == "END_PROGRAM")
                        {
                            SendSystemInfo($"配信が終了したため切断します。", InfoType.Notice);
                            Disconnect();
                        }
                        else
                        {
                            SendSystemInfo($"コメントデータサーバーとの接続が切断されました{Environment.NewLine}原因:{disconnect.Reason}", InfoType.Notice);
                            //Disconnect();
                        }
                        break;
                    case Metadata.ServerTime serverTime:
                        break;
                    case Metadata.MessageServer messageServer:
                        if(_messageServerClient != null)
                        {
                           _messageServerClient.disconnect();
                        }
                        Debug.WriteLine(messageServer.MessageServerUrl);
                        _messageServerClient = new MessageServerClient(messageServer.MessageServerUrl, ProcessChunkedEntry);
                        var task = _messageServerClient.doConnect();
                        _toAdd.Add(task);
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveDisconnectedServers()
        {
            // isdisconnectがtrueの要素を削除
            _segmentServers = _segmentServers.Where(server => !server.isDisconnect).ToList();
        }

        public List<SegmentServerClient>? _segmentServers = null;

        public async Task ProcessChunkedEntry(ChunkedEntry entry)
        {
            if(_messageServerClient == null)
            {
                return;
            }
            if (entry.Next != null)
            {
                //&at=の値を更新
                _messageServerClient.NextStreamAt = entry.Next.At.ToString();
            }
            else if(entry.Previous != null)
            {
                //コメント取得に関係ないので無視
            }
            else if (entry.Backward != null)
            {
                //コメント取得に関係ないので無視
            }
            else if (entry.Segment != null)
            {
                string Uri = entry.Segment.Uri;
                if(Uri == null)
                {
                    await Task.CompletedTask;
                }
                if(_segmentServers == null)
                {
                    _segmentServers = new List<SegmentServerClient> ();
                }

                RemoveDisconnectedServers();

                //デバック用
                //名札付きコメント
                //string base64String = "Cj4KJEVoa0tFZ2sxYzctSzIxbVJBUkZVMkdhZVVHcFVqQkNnMDVjShIMCIXh+7UGEMiC64gDGggKBgiCnOKkARI3CjUKG+ebuOaJi+OBr+epuuOBqOa1t+OBoOOBi+OCiRIG44K/44ONGPXpoAwoi5zhFjoAQIPdBQ==";
                //匿名コメント
                //string base64String = "Cj4KJEVoa0tFZ2tTZG9KdDIxbVJBUkVkRXYwN0x1YTZqUkNnMDVjShIMCP7g+7UGELjpyaEBGggKBgiCnOKkARJlCmMKQOOCguOBl+OBi+OBl+OBpuOBvuOBoOatjOiInuS8juW6p+OCv+ODr+ODvOOBq+WFpeOCjOOBquOBhOOBru+8n3cYtuegDCABMhJhOm9QanZWSHEwN2txa3k1c206AECA3QU=";
                //22位にランクインしました
                //string base64String = "Cj4KJEVoa0tFZ25iZmhQbkcxdVJBUkVseUdrSF9yUWd2eEN3OF9rSxIMCIiF/bUGEICjhNMDGggKBgjJ2eOkARIqOigyJuesrDIy5L2N44Gr44Op44Oz44Kv44Kk44Oz44GX44G+44GX44Gf";
                //「縁日射的」がリクエストされました
                //string base64String = "Cj0KJEVoa0tFZ2wtZFdyeVBsbVJBUkhiejZsZnZoNjZoeENVanQ0SxILCO+Q+7UGEPj2wGQaCAoGCPfB46QBEjc6NQoz44CM57iB5pel5bCE55qE44CN44GM44Oq44Kv44Ko44K544OI44GV44KM44G+44GX44Gf";
                //匿名プレミアム
                //string base64String = "Cj4KJEVoa0tFZ240YzlkaXgxMlJBUkVGT3Q3VEpKWU5saENFMHEwTBIMCIHj/7UGEPCHwOYCGggKBgiUheSkARIwCi4KDmh0dHAgY2xpZW5077yfGIXKBSABMhJhOjZMWHZ4ZEFPR1Q4V0RCZUM6AEAD";
                //名札プレミアム
                //string base64String = "Cj4KJEVoa0tFZ25nZVpjaEcxdVJBUkZsb05PRU9pQUNsQkN3OF9rSxIMCNaE/bUGENDy8skBGggKBgjJ2eOkARIwCi4KFemZveawl+OBqu+8ou+8p++8reOBjBIFR29oZXkYraQKIAEo5+ejAjoAQMAC";
                //「ゲーム」が好きな2人が来場しました
                //string base64String = "Cj4KJEVoa0tFZ25BY1d0aUhGdVJBUkUwWVJ4SEFISW1weEN3OF9rSxIMCKiF/bUGEOjI54gCGggKBgjJ2eOkARI4OjY6NOOAjOOCsuODvOODoOOAjeOBjOWlveOBjeOBqjLkurrjgYzmnaXloLTjgZfjgb7jgZfjgZ8=";
                // 3時間延長しました
                //string base64String = "Cj4KJEVoa0tFZ2tlYzYzZ2gyT1JBUkVpNmVWNnpLZjJzQkRUNVlnTRIMCPbUhbYGEJiU0cgDGggKBgjbzuSkARIdOhsqGTPmmYLplpPlu7bplbfjgZfjgb7jgZfjgZ8=";
                //運営、放送者コメント(リンクあり)
                //string base64String = "Cj4KJEVoa0tFZ21SY3hvSHFsbVJBUkZPdE1ReVVXUGFtUkNoMDVjShIMCNjH+7UGEPCEpZkDGggKBgiCnOKkASLwAiLtAgrqAgrnAgq4AuS9nOOBo+OBn+OCv+ODreODg+ODiOOBp+WNoOOBo+OBn+e1kOaenOOBjOS6uuOAheOBrumBi+WRveOCkuW3puWPs+OBmeOCi+ODh+ODg+OCreani+eviUFEVuOAjlRoZSBDb3NtaWMgV2hlZWwgU2lzdGVyaG9vZOOAj+OBjFN0ZWFt44Gr44GmNDAl44Kq44OV44GuMTIwMOWGhuOBq+OAguWNoOOBhOOBruWKm+OCkuaMgeOBpOmtlOWls+OBqOOBl+OBpuOCquODquOCuOODiuODq+OBruOCv+ODreODg+ODiOOCkuS9nOOCiuOAgeW8leOBhOOBn+OCq+ODvOODieOBruOAjOino+mHiOOAjeOBp+ebuOaJi+OBrumBi+WRveOBjOWkieOCj+OBo+OBpuOBhOOBjyIqaHR0cHM6Ly9uZXdzLm5pY292aWRlby5qcC93YXRjaC9udzE2MjU3NTMy";
                //放送者コメント(リンクなし)
                //string base64String = "Cj4KJEVoa0tFZ242ZEVqNG9HT1JBUkhBUmlGZE9iOHdnUkRVNVlnTRIMCOPhhbYGEID84b4BGggKBgjbzuSkASI/Ij0KOwo1CjPpm7vlrZDjg6zjg7PjgrjjgafjgrPjg6Hjg5Pjg6XokL3jgaHjgabjgZ/jgZnjgb7jgpMaAggP";
                //投票開始
                //string base64String = "Cj4KJEVoa0tFZ21sZVpCV0pWbVJBUkdGX3RJa2hPT0V1eENPdC1ZSxIMCOCD+7UGEODnmbMDGggKBgjMyOOkASJCEkAKD+OCouODs+OCseODvOODiBIOCgzjg6njg7zjg6Hjg7MSCwoJ44GG44Gp44KTEg4KDOOBneOBhuOCgeOCkxgB";
                //投票結果
                //string base64String = "Cj4KJEVoa0tFZ25IY21ka0pWbVJBUkYwU2RSMEpPMFVyaENPdC1ZSxIMCOSD+7UGENDE/tgBGggKBgjMyOOkASJJEkcKD+OCouODs+OCseODvOODiBIQCgzjg6njg7zjg6Hjg7MYABINCgnjgYbjganjgpMYABIRCgzjgZ3jgYbjgoHjgpMY6AcYAg==";
                //投票結果非表示(エラーなく無視する必要がある)
                //string base64String = "Cj4KJEVoa0tFZ2x4Zng2MkhGbVJBUkdkQzVvQ0FGOUlxUkNPdC1ZSxIMCKv/+rUGEND7tYYCGggKBgjMyOOkASICEgA=";

                //Base64文字列をbyte[] に変換
                //byte[] byteArray = Convert.FromBase64String(base64String);
                //var chunkedMessage = ChunkedMessage.Parser.ParseFrom(byteArray);
                //await ProcessChunkedMessage(chunkedMessage);

                var segmentServer = new SegmentServerClient(Uri, ProcessChunkedMessage);
                _segmentServers.Add(segmentServer);
                var task = segmentServer.doConnect();
                _toAdd.Add(task);
                _toAdd.RemoveAll(task => task.IsCompleted);//使い終わったSegmentServerClientがメモリにたまっていくので消す
            }
            await Task.CompletedTask;
        }

        public async Task ProcessChunkedMessage(ChunkedMessage message)
        {
            if (_messageServerClient == null)
            {
                return;
            }
            if (message.Message?.SimpleNotification != null)
            {
                var notification = message.Message?.SimpleNotification;
                //info
                if (notification.ProgramExtended != null && notification.ProgramExtended != "")
                {
                    var contents = notification.ProgramExtended;

                    var date = Now();
                    if (message.Meta?.At != null)
                    {
                        date = fixDateTime(message.Meta.At.ToDateTime());
                    }

                    var comment = new NicoInfo(contents)
                    {
                        Text = contents,
                        PostedAt = date
                    };
                    var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                    {
                        IsInitialComment = _isInitialCommentsReceiving,
                        SiteContextGuid = SiteContextGuid,
                    };
                    var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                    RaiseMessageReceived(context);
                }
                else if (notification.Ichiba != null && notification.Ichiba != "")
                {
                    var Ichiba = "【放送ネタ】" + notification.Ichiba;

                    var date = Now();
                    if (message.Meta?.At != null)
                    {
                        date = fixDateTime(message.Meta.At.ToDateTime());
                    }

                    var comment = new NicoSpi(Ichiba)
                    {
                        Text = Ichiba,
                        PostedAt = date,
                    };
                    var metadata = new SpiMessageMetadata(comment, _options, _siteOptions)
                    {
                        IsInitialComment = _isInitialCommentsReceiving,
                        SiteContextGuid = SiteContextGuid,
                    };
                    var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                    RaiseMessageReceived(context);
                }
                else if (notification.RankingIn != null && notification.RankingIn != "")
                {
                    var contents = notification.RankingIn;

                    var date = Now();
                    if (message.Meta?.At != null)
                    {
                        date = fixDateTime(message.Meta.At.ToDateTime());
                    }
                    var comment = new NicoInfo(contents)
                    {
                        Text = contents,
                        PostedAt = date,
                    };
                    var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                    {
                        IsInitialComment = _isInitialCommentsReceiving,
                        SiteContextGuid = SiteContextGuid,
                    };
                    var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                    RaiseMessageReceived(context);
                }
                else if (notification.Visited != null && notification.Visited != "")
                {
                    var contents = notification.Visited;
                    var date = Now();
                    if (message.Meta?.At != null)
                    {
                        date = fixDateTime(message.Meta.At.ToDateTime());
                    }
                    var comment = new NicoInfo(contents)
                    {
                        Text = contents,
                        PostedAt = date
                    };
                    var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                    {
                        IsInitialComment = _isInitialCommentsReceiving,
                        SiteContextGuid = SiteContextGuid,
                    };
                    var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                    RaiseMessageReceived(context);
                }
            }
            if(message.Message?.Gift != null)
            {
                SendSystemInfo("ギフトを検知しましたが、このバージョンでは対応していません", InfoType.Error);
                var contents = "ギフトを検知しました";
                var comment = new NicoInfo(contents)
                {
                    Text = contents,
                    PostedAt = Now(),
                };
                var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                {
                    IsInitialComment = _isInitialCommentsReceiving,
                    SiteContextGuid = SiteContextGuid,
                };
                var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                RaiseMessageReceived(context);
            }
            if (message.Message?.Nicoad != null)
            {
                SendSystemInfo("ニコニ広告を検知しましたが、このバージョンでは対応していません", InfoType.Error);
                var contents = "ニコニ広告を検知しました";
                var comment = new NicoInfo(contents)
                {
                    Text = contents,
                    PostedAt = Now(),
                };
                var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                {
                    IsInitialComment = _isInitialCommentsReceiving,
                    SiteContextGuid = SiteContextGuid,
                };
                var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                RaiseMessageReceived(context);
            }
            if(message.State != null)
            {
                var announce = message.State?.Marque?.Display?.OperatorComment; //放送者コメント
                if(announce != null)
                {
                    var contents = announce.Content;
                    if (announce.Link != null && announce.Link != "")
                    {
                        contents = announce.Content + "(" + announce.Link + ")";
                    }
                    var comment = new NicoInfo(contents)
                    {
                        Text = contents,
                        PostedAt = Now(),
                    };
                    var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                    {
                        IsInitialComment = _isInitialCommentsReceiving,
                        SiteContextGuid = SiteContextGuid,
                    };
                    var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                    RaiseMessageReceived(context);
                }

                if (message.State.Enquete != null)
                {
                    if(message.State.Enquete?.Question == null||message.State.Enquete?.Question == "")
                    {
                        return;
                    }
                    var vote = message.State.Enquete;
                    if(!vote.Status.Equals(Status.Closed)) //0 = closedは送られてこないので無視
                    {
                        var question = vote.Question;
                        var status = "result";
                        if (vote.Status.Equals(Status.Poll))
                        {
                            status = "start";
                        }
                        var contents = "/vote " + status + " " + question + " ";


                        foreach(var item in vote.Choices)
                        {
                            var description = item.Description;
                            if (status == "result")
                            {
                                contents += description + "("+ ToPercentage(item.PerMille) + ") ";
                            }else{
                                contents += description + " ";
                            }
                           
                           
                        }

                        contents = contents.Trim();

                        var date = Now();
                        if (message.Meta?.At != null)
                        {
                            date = fixDateTime(message.Meta.At.ToDateTime());
                        }

                        var comment = new NicoInfo(contents)
                        {
                            Text = contents,
                            PostedAt = date,
                        };
                        var metadata = new InfoMessageMetadata(comment, _options, _siteOptions)
                        {
                            IsInitialComment = _isInitialCommentsReceiving,
                            SiteContextGuid = SiteContextGuid,
                        };
                        var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                        RaiseMessageReceived(context);
                    }
                }

            }
            if (message.Message?.Chat != null)
            {
                var chat = message.Message.Chat;
                var content = chat.Content;
                var vpos = chat.Vpos;
                string userId = "";
                if (chat.RawUserId != 0&& chat.Name != null && chat.Name != "")
                {
                    userId = chat.Name + "(" + chat.RawUserId.ToString() + ")";
                }
                var name = "";
                if (chat.HashedUserId != null && chat.HashedUserId != "")
                {
                    userId = chat.HashedUserId.Substring(2);//a:
                }
               
               
                var anonymity = true;
                var no = chat.No;
                if (chat.Name != null&&chat.Name != "")
                {
                    name = chat.Name;// + "(" + chat.RawUserId + ")";
                    anonymity = false;
                }
                var isPremium = chat.AccountStatus == 0 ? "normal" : "premium";
                if (anonymity)
                {
                    if (userId != "")
                    {

                        if (isPremium == "premium")
                        {
                            userId = "a:premium:" + userId;
                        }
                        else
                        {
                            userId = "a:" + userId;
                        }
                    }
                }
                else
                {
                    if (userId != "")
                    {
                        if (isPremium == "premium")
                        {
                            userId = userId + "(premium)";
                        }
                    }
                }
                var at = Now();
                if (message.Meta?.At != null)
                {
                    at = fixDateTime(message.Meta.At.ToDateTime());
                }

                string? thumbNailUrl = null;
                if (chat.RawUserId != 0)
                {
                    thumbNailUrl = GetThumbnail(chat.RawUserId.ToString());
                }
                //if(userId == null)
                //{
                //    userId = name;
                //}
               

                var comment = new NicoComment("")
                {
                    ChatNo = chat.No,
                    Id = chat.No.ToString(),
                    Is184 = anonymity,
                    PostedAt = at,
                    Text = content,
                    UserId = userId,
                    UserName = name,
                    ThumbnailUrl = thumbNailUrl,
                };

                var user = GetUser(userId);
                bool isFirstComment;
                if (_userCommentCountDict.ContainsKey(userId))
                {
                    _userCommentCountDict[userId]++;
                    isFirstComment = false;
                }
                else
                {
                    _userCommentCountDict.AddOrUpdate(userId, 1, (s, n) => n);
                    isFirstComment = true;
                }

                var metadata = new CommentMessageMetadata(comment, _options, _siteOptions, user, this, isFirstComment)
                {
                    IsInitialComment = _isInitialCommentsReceiving,
                    SiteContextGuid = SiteContextGuid,
                };

                var context = new NicoMessageContext(comment, metadata, new NicoMessageMethods());
                RaiseMessageReceived(context);
            }

            await Task.CompletedTask;
        }
        public DateTime fixDateTime(DateTime utcTime)
        {
            return utcTime;//不要だったけど放置
        }

        public string ToPercentage(int? number)
        {
            if (number == null||number == 0)
            {
                return "0.0%";
            }

            // パーセント計算
            double percentage = ((double)number / 1000) * 100;

            // 結果を小数点以下1桁でフォーマット
            return $"{percentage:F1}%";
        }

        public DateTime Now()
        {
            var utcNow = DateTime.UtcNow;

            // 日本標準時（JST）のタイムゾーン情報を取得
            var jstZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

            // UTC日時を日本標準時（JST）に変換
            return TimeZoneInfo.ConvertTimeFromUtc(utcNow, jstZone);
        }

        DateTime? _vposBaseTime;
        DateTime? _localTime;
        /// <summary>
        /// 意図的な切断
        /// </summary>
        public override void Disconnect()
        {
            _isDisconnectedExpected = true;
            _disconnectCts.Cancel();
            _metaProvider?.Disconnect();
            _chatProvider?.Disconnect();
            _messageServerClient?.disconnect();
            _messageServerClient = null;
            if (_segmentServers != null)
            {
                foreach (var server in _segmentServers)
                {
                    server.disconnect();
                }
                _segmentServers = new List<SegmentServerClient>();
            }
        }

        public override async Task<ICurrentUserInfo> GetCurrentUserInfo(IBrowserProfile browserProfile)
        {
            var cc = GetCookieContainer(browserProfile);
            try
            {
                var myInfo = await Api.GetMyInfo(_server, cc);
                return await Task.FromResult(new CurrentUserInfo { Username = myInfo.Nickname, IsLoggedIn = myInfo.IsLogin });
            }
            catch (NotLoggedInException)
            {
                return await Task.FromResult(new CurrentUserInfo { Username = "(未ログイン)", IsLoggedIn = false });
            }
        }
        class CurrentUserInfo : ICurrentUserInfo
        {
            public string Username { get; set; }
            public bool IsLoggedIn { get; set; }
        }
        public override IUser GetUser(string userId)
        {
            return _userStoreManager.GetUser(SiteType.NicoLive, userId);
        }

        public override Task PostCommentAsync(string text)
        {
            throw new NotImplementedException();
        }

        public override void SetMessage(string raw)
        {
        }

        Task INicoCommentProvider.PostCommentAsync(string comment, bool is184, string color, string size, string position)
        {
            var elapsed = DateTime.Now.AddHours(-9) - _vposBaseTime.Value;
            var ms = elapsed.TotalMilliseconds;
            var vpos = (long)Math.Round(ms / 10);
            var postComment = new PostComment(comment, vpos, is184, color, size, position);
            _metaProvider.Send(postComment);
            return Task.CompletedTask;
        }
        public TestCommentProvider(ICommentOptions options, INicoSiteOptions siteOptions, IDataSource server, ILogger logger, IUserStoreManager userStoreManager) : base(logger, options)
        {
            _logger = logger;
            _userStoreManager = userStoreManager;
            _siteOptions = siteOptions;
            _server = server;
            _metaProvider = new Metadata.MetaProvider(_logger);
            _metaProvider.Received += MetaProvider_Received;
            _chatProvider = new Chat.ChatProvider(_logger);
            _chatProvider.Received += ChatProvider_Received;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _metaProvider.Received -= MetaProvider_Received;
                    _chatProvider.Received -= ChatProvider_Received;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TestCommentProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
