2026-06-29

- TwitchSitePlugin の USERNOTICE 通知を実装した。
- USERNOTICE は既存の TwitchNotice として通知し、system-msg と任意のユーザー入力メッセージを連結して表示する。サブスク系だけでなく raid や ritual なども system-msg があれば表示対象にする。
- Tools.Parse で IRC タグ値のエスケープを解除し、タグキー検索とコマンド名の大小差を吸収するようにした。
- 接続中に Input の自動サイト選択やサイト選択UIの書き戻しが走ると ConnectionViewModel.SelectedSite の assert に当たるため、接続中のサイト変更は無視してUIへ現在値を通知し直すようにした。
- Twitch Notice の既定色は黄色背景に白文字で読みにくいため、既定の NoticeForeColor を黒にした。
- USERNOTICE の viewermilestone かつ msg-param-category=watch-streak は連続視聴記録通知なので表示しない。
- Twitch Notice/USERNOTICE は SubscriptionNoticeBackColor/SubscriptionNoticeForeColor で表示する。サブスク判定が外れても黄色に戻らないよう、旧 NoticeBackColor/NoticeForeColor は表示経路では使わない。既定は紫背景 #FF5E35B1、白文字。
- USERNOTICE に userMessage がある場合は system-msg だけを TwitchNotice として先に表示し、userMessage は後続の TwitchComment として流す。これによりユーザーメッセージ部分は通常コメント扱いになり、BouyomiPlugin の新規タイプ追加は不要。
- Twitch USERNOTICE は設定で全停止、既知 msg-id ごとの受信、その他の受信を切り替えられる。判定は受信時に _siteOptions を読むため、適用済み設定は再接続なしで次の USERNOTICE から反映される。
