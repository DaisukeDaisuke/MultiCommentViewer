2026-06-29

- TwitchSitePlugin の USERNOTICE 通知を実装した。
- USERNOTICE は既存の TwitchNotice として通知し、system-msg と任意のユーザー入力メッセージを連結して表示する。サブスク系だけでなく raid や ritual なども system-msg があれば表示対象にする。
- Tools.Parse で IRC タグ値のエスケープを解除し、タグキー検索とコマンド名の大小差を吸収するようにした。
- 接続中に Input の自動サイト選択やサイト選択UIの書き戻しが走ると ConnectionViewModel.SelectedSite の assert に当たるため、接続中のサイト変更は無視してUIへ現在値を通知し直すようにした。
