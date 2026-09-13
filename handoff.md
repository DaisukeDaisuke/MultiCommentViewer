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

2026-07-05

- NicoSitePlugin2 の IsShow184Id 既定値を true、IsAutoGetUsername 既定値を false に変更した。保存済み設定に値がある場合は DynamicOptionsBase.Deserialize が保存値を優先する。
- NicoSitePlugin2 に ProgramExtended/Ichiba/RankingIn/Visited/Cruise/Emotion/SupporterRegistered/UserLevelUp/Gift/Nicoad/OperatorComment/Vote の受信可否設定を追加した。既定値は既存挙動維持のためすべて true。
- 受信可否は TestCommentProvider.ProcessChunkedMessage 内で _siteOptions を都度参照するため、設定反映後は再接続なしで次の受信から効く。
- 旧 ChatProvider 経由の WebSocket 二重接続処理を TestCommentProvider から外した。現行 MessageServer/SegmentServer/PackedSegmentClient 経路は維持し、メインループ起床は従来通り Room メタ情報受信側で行う。

2026-09-13

- コミット a4311c8fc3b325c91faf60ed356dc3ef6ad3869b の MainWindow.xaml 差分を逆向きに適用し、追加されたログイン列を削除して後続列の既定 DisplayIndex を元に戻した。
- ConnectionViewModel から LoginCommand、CanLogin、ログイン可否判定、ログインURL取得、ログイン実行処理と、それらに伴う状態更新・不要な using を削除した。ログインユーザ名表示や既存の接続・切断処理は維持した。
- YouTubeLiveSiteOptions に IsHideMembershipIcon（既定値 false）を追加した。設定画面の「メンバーシップアイコンを非表示にする」を有効にすると、カスタムサムネイル形式の投稿者バッジだけをメッセージ部品の生成前に除外する。
- メンバーアイコン非表示設定は各メッセージ生成時に _siteOptions から読むため、適用後は再接続なしで次に挿入されるコメントから反映される。投稿者名やモデレーターバッジなどは維持され、空の画像部品も生成しない。
