using System;
using System.ComponentModel;
using System.Windows.Media;

namespace TwitchSitePlugin
{
    class TwitchSiteOptionsViewModel : INotifyPropertyChanged
    {
        public bool NeedAutoSubNickname
        {
            get => changed.NeedAutoSubNickname;
            set => changed.NeedAutoSubNickname = value;
        }
        public string NeedAutoSubNicknameStr
        {
            get => changed.NeedAutoSubNicknameStr;
            set => changed.NeedAutoSubNicknameStr = value;
        }
        public Color NoticeBackColor
        {
            get { return ChangedOptions.NoticeBackColor; }
            set { ChangedOptions.NoticeBackColor = value; }
        }
        public Color NoticeForeColor
        {
            get { return ChangedOptions.NoticeForeColor; }
            set { ChangedOptions.NoticeForeColor = value; }
        }
        public Color SubscriptionNoticeBackColor
        {
            get { return ChangedOptions.SubscriptionNoticeBackColor; }
            set { ChangedOptions.SubscriptionNoticeBackColor = value; }
        }
        public Color SubscriptionNoticeForeColor
        {
            get { return ChangedOptions.SubscriptionNoticeForeColor; }
            set { ChangedOptions.SubscriptionNoticeForeColor = value; }
        }
        public bool IgnoreAllUserNotices
        {
            get => ChangedOptions.IgnoreAllUserNotices;
            set => ChangedOptions.IgnoreAllUserNotices = value;
        }
        public bool ReceiveUserNoticeAnongiftpaidupgrade
        {
            get => ChangedOptions.ReceiveUserNoticeAnongiftpaidupgrade;
            set => ChangedOptions.ReceiveUserNoticeAnongiftpaidupgrade = value;
        }
        public bool ReceiveUserNoticeAnonsubgift
        {
            get => ChangedOptions.ReceiveUserNoticeAnonsubgift;
            set => ChangedOptions.ReceiveUserNoticeAnonsubgift = value;
        }
        public bool ReceiveUserNoticeAnonsubmysterygift
        {
            get => ChangedOptions.ReceiveUserNoticeAnonsubmysterygift;
            set => ChangedOptions.ReceiveUserNoticeAnonsubmysterygift = value;
        }
        public bool ReceiveUserNoticeGiftpaidupgrade
        {
            get => ChangedOptions.ReceiveUserNoticeGiftpaidupgrade;
            set => ChangedOptions.ReceiveUserNoticeGiftpaidupgrade = value;
        }
        public bool ReceiveUserNoticeRaid
        {
            get => ChangedOptions.ReceiveUserNoticeRaid;
            set => ChangedOptions.ReceiveUserNoticeRaid = value;
        }
        public bool ReceiveUserNoticeResub
        {
            get => ChangedOptions.ReceiveUserNoticeResub;
            set => ChangedOptions.ReceiveUserNoticeResub = value;
        }
        public bool ReceiveUserNoticeRitual
        {
            get => ChangedOptions.ReceiveUserNoticeRitual;
            set => ChangedOptions.ReceiveUserNoticeRitual = value;
        }
        public bool ReceiveUserNoticeSub
        {
            get => ChangedOptions.ReceiveUserNoticeSub;
            set => ChangedOptions.ReceiveUserNoticeSub = value;
        }
        public bool ReceiveUserNoticeSubgift
        {
            get => ChangedOptions.ReceiveUserNoticeSubgift;
            set => ChangedOptions.ReceiveUserNoticeSubgift = value;
        }
        public bool ReceiveUserNoticeSubmysterygift
        {
            get => ChangedOptions.ReceiveUserNoticeSubmysterygift;
            set => ChangedOptions.ReceiveUserNoticeSubmysterygift = value;
        }
        public bool ReceiveUserNoticeOther
        {
            get => ChangedOptions.ReceiveUserNoticeOther;
            set => ChangedOptions.ReceiveUserNoticeOther = value;
        }
        private readonly TwitchSiteOptions _origin;
        private readonly TwitchSiteOptions changed;
        internal TwitchSiteOptions OriginOptions { get { return _origin; } }
        internal TwitchSiteOptions ChangedOptions { get { return changed; } }

        internal TwitchSiteOptionsViewModel(TwitchSiteOptions siteOptions)
        {
            _origin = siteOptions;
            changed = siteOptions.Clone();
        }

        #region INotifyPropertyChanged
        [NonSerialized]
        private System.ComponentModel.PropertyChangedEventHandler _propertyChanged;
        /// <summary>
        /// 
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _propertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
