using System.ComponentModel;
using System.Windows.Media;

namespace TwitchSitePlugin
{
    interface ITwitchSiteOptions : INotifyPropertyChanged
    {
        bool NeedAutoSubNickname { get; }
        string NeedAutoSubNicknameStr { get; }
        Color NoticeBackColor { get; }
        Color NoticeForeColor { get; }
        Color SubscriptionNoticeBackColor { get; }
        Color SubscriptionNoticeForeColor { get; }
        bool IgnoreAllUserNotices { get; }
        bool ReceiveUserNoticeAnongiftpaidupgrade { get; }
        bool ReceiveUserNoticeAnonsubgift { get; }
        bool ReceiveUserNoticeAnonsubmysterygift { get; }
        bool ReceiveUserNoticeGiftpaidupgrade { get; }
        bool ReceiveUserNoticeRaid { get; }
        bool ReceiveUserNoticeResub { get; }
        bool ReceiveUserNoticeRitual { get; }
        bool ReceiveUserNoticeSub { get; }
        bool ReceiveUserNoticeSubgift { get; }
        bool ReceiveUserNoticeSubmysterygift { get; }
        bool ReceiveUserNoticeOther { get; }
    }
}
