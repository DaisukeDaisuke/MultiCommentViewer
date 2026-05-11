using System.Threading.Tasks;
using System.Net;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YouTubeLiveSitePlugin.Test2;
using System.Text.RegularExpressions;

namespace YouTubeLiveSitePlugin
{
    internal static class ChannelLiveFinder
    {
        public static async Task<List<string>> FindLiveVidsAsync(IYouTubeLiveServer server, YouTubeLiveSitePlugin.Input.IChannelUrl channelUrl)
        {
            var url = $"{channelUrl.Raw}/streams";
            var html = await server.GetEnAsync(url);//2022/11/30 Cookieを渡してしまうとアカウント設定かなんかのせいで強制的に英語にすることができなかった。
            var ytInitialDataStr = Tools.ExtractYtInitialDataFromChannelHtml(html);
            dynamic ytInitialData = JsonConvert.DeserializeObject(ytInitialDataStr);
            //"Live"タブを探す
            var liveTab = GetLiveTab(ytInitialData);

            var list = new List<string>();
            //"LIVE"ラベルが付いている動画を探す
            foreach (var content in liveTab.tabRenderer.content.richGridRenderer.contents)
            {
                if (!content.ContainsKey("richItemRenderer"))
                {
                    //一番最後の項目はcontinuationItemRendererになっていた。更に表示する用かと。
                    continue;
                }

                // 変更1: 新構造(lockupViewModel)と旧構造(videoRenderer)の両方に対応
                string videoId;
                bool isLive;
                var itemContent = content.richItemRenderer.content;
                if (itemContent.ContainsKey("lockupViewModel"))
                {
                    // 新構造: videoIdはwatchEndpointから取得
                    var lockup = itemContent.lockupViewModel;
                    var overlays = lockup.contentImage.thumbnailViewModel.overlays;
                    isLive = false;
                    videoId = null;
                    foreach (var overlay in overlays)
                    {
                        if (overlay.ContainsKey("thumbnailBottomOverlayViewModel"))
                        {
                            var badges = overlay.thumbnailBottomOverlayViewModel.badges;
                            foreach (var badge in badges)
                            {
                                if (badge.ContainsKey("thumbnailBadgeViewModel"))
                                {
                                    var badgeStyle = (string)badge.thumbnailBadgeViewModel.badgeStyle;
                                    if (badgeStyle == "THUMBNAIL_OVERLAY_BADGE_STYLE_LIVE")
                                    {
                                        isLive = true;
                                        // animationActivationTargetIdがvideoIdそのもの
                                        videoId = (string)badge.thumbnailBadgeViewModel.animationActivationTargetId;
                                    }
                                }
                            }
                        }
                    }
                    if (isLive && videoId != null)
                    {
                        list.Add(videoId);
                    }
                }
                else
                {
                    // 旧構造: videoRenderer
                    videoId = (string)itemContent.videoRenderer.videoId;
                    var thumbnailText = (string)(GetText(itemContent.videoRenderer.thumbnailOverlays[0].thumbnailOverlayTimeStatusRenderer.text) ?? "");
                    if (thumbnailText == "LIVE")
                    {
                        list.Add(videoId);
                    }
                }
            }
            return list;
        }
        private static string? GetText(dynamic d)
        {
            if (d.ContainsKey("runs"))
            {
                var title = "";
                var runs = d.runs;
                foreach (var run in runs)
                {
                    if (run.ContainsKey("text"))
                    {
                        title += (string)run.text;
                    }
                }
                return title;
            }
            else if (d.ContainsKey("simpleText"))
            {
                return d.simpleText;
            }
            else
            {
                return null;
            }
        }
        private static dynamic GetLiveTab(dynamic ytInitialData)
        {
            var tabs = ytInitialData.contents.twoColumnBrowseResultsRenderer.tabs;
            foreach (var tab in tabs)
            {
                string title;
                bool selected;
                if (tab.ContainsKey("tabRenderer"))
                {
                    title = (string)tab.tabRenderer.title;
                    selected = tab.tabRenderer.selected ?? false;
                }
                else
                {
                    throw new Exception();
                }
                // 変更3: "ライブ"（日本語）にも対応
                if ((title == "Live" || title == "ライブ") && selected)
                {
                    return tab;
                }
            }
            throw new Exception();
        }
    }
}
