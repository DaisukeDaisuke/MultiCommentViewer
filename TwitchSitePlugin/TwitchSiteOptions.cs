using Common;
using System;
using System.Windows.Media;

namespace TwitchSitePlugin
{
    internal class TwitchSiteOptions : DynamicOptionsBase, ITwitchSiteOptions
    {
        /// <summary>
        /// @コテハンを自動登録するか
        /// </summary>
        public bool NeedAutoSubNickname { get => GetValue(); set => SetValue(value); }
        public string NeedAutoSubNicknameStr { get => GetValue(); set => SetValue(value); }
        public Color NoticeBackColor { get => GetValue(); set => SetValue(value); }
        public Color NoticeForeColor { get => GetValue(); set => SetValue(value); }
        public Color SubscriptionNoticeBackColor { get => GetValue(); set => SetValue(value); }
        public Color SubscriptionNoticeForeColor { get => GetValue(); set => SetValue(value); }
        public bool IgnoreAllUserNotices { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeAnongiftpaidupgrade { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeAnonsubgift { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeAnonsubmysterygift { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeGiftpaidupgrade { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeRaid { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeResub { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeRitual { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeSub { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeSubgift { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeSubmysterygift { get => GetValue(); set => SetValue(value); }
        public bool ReceiveUserNoticeOther { get => GetValue(); set => SetValue(value); }
        protected override void Init()
        {
            Dict.Add(nameof(NeedAutoSubNickname), new Item { DefaultValue = false, Predicate = b => true, Serializer = b => b.ToString(), Deserializer = s => bool.Parse(s) });
            Dict.Add(nameof(NeedAutoSubNicknameStr), new Item { DefaultValue = "@|＠", Predicate = s => !string.IsNullOrEmpty(s), Serializer = s => s, Deserializer = s => s });
            Dict.Add(nameof(NoticeBackColor), new Item { DefaultValue = ColorFromArgb("#FFFFFF00"), Predicate = c => true, Serializer = c => ColorToArgb(c), Deserializer = s => ColorFromArgb(s) });
            Dict.Add(nameof(NoticeForeColor), new Item { DefaultValue = ColorFromArgb("#FF000000"), Predicate = c => true, Serializer = c => ColorToArgb(c), Deserializer = s => ColorFromArgb(s) });
            Dict.Add(nameof(SubscriptionNoticeBackColor), new Item { DefaultValue = ColorFromArgb("#FF5E35B1"), Predicate = c => true, Serializer = c => ColorToArgb(c), Deserializer = s => ColorFromArgb(s) });
            Dict.Add(nameof(SubscriptionNoticeForeColor), new Item { DefaultValue = ColorFromArgb("#FFFFFFFF"), Predicate = c => true, Serializer = c => ColorToArgb(c), Deserializer = s => ColorFromArgb(s) });
            Dict.Add(nameof(IgnoreAllUserNotices), CreateBoolItem(false));
            Dict.Add(nameof(ReceiveUserNoticeAnongiftpaidupgrade), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeAnonsubgift), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeAnonsubmysterygift), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeGiftpaidupgrade), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeRaid), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeResub), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeRitual), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeSub), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeSubgift), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeSubmysterygift), CreateBoolItem(true));
            Dict.Add(nameof(ReceiveUserNoticeOther), CreateBoolItem(true));
        }
        internal TwitchSiteOptions Clone()
        {
            return (TwitchSiteOptions)this.MemberwiseClone();
        }
        internal void Set(TwitchSiteOptions changedOptions)
        {
            foreach (var src in changedOptions.Dict)
            {
                var v = src.Value;
                SetValue(v.Value, src.Key);
            }
        }
        #region Converters
        private Color ColorFromArgb(string argb)
        {
            if (argb == null)
                throw new ArgumentNullException("argb");
            var pattern = "#(?<a>[0-9a-fA-F]{2})(?<r>[0-9a-fA-F]{2})(?<g>[0-9a-fA-F]{2})(?<b>[0-9a-fA-F]{2})";
            var match = System.Text.RegularExpressions.Regex.Match(argb, pattern, System.Text.RegularExpressions.RegexOptions.Compiled);

            if (!match.Success)
            {
                throw new ArgumentException("形式が不正");
            }
            else
            {
                var a = byte.Parse(match.Groups["a"].Value, System.Globalization.NumberStyles.HexNumber);
                var r = byte.Parse(match.Groups["r"].Value, System.Globalization.NumberStyles.HexNumber);
                var g = byte.Parse(match.Groups["g"].Value, System.Globalization.NumberStyles.HexNumber);
                var b = byte.Parse(match.Groups["b"].Value, System.Globalization.NumberStyles.HexNumber);
                return Color.FromArgb(a, r, g, b);
            }
        }
        private string ColorToArgb(Color color)
        {
            var argb = color.ToString();
            return argb;
        }
        private Item CreateBoolItem(bool defaultValue)
        {
            return new Item { DefaultValue = defaultValue, Predicate = b => true, Serializer = b => b.ToString(), Deserializer = s => bool.Parse(s) };
        }
        #endregion
    }
}
