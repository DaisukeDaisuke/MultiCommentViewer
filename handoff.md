2026-06-29

- TwitchSitePlugin の USERNOTICE 通知を実装した。
- USERNOTICE は既存の TwitchNotice として通知し、system-msg と任意のユーザー入力メッセージを連結して表示する。サブスク系だけでなく raid や ritual なども system-msg があれば表示対象にする。
- Tools.Parse で IRC タグ値のエスケープを解除し、タグキー検索とコマンド名の大小差を吸収するようにした。
