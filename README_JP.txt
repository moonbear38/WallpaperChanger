WallpaperChanger

シンプルな Windows 用壁紙スライドショーアプリです。

【特徴】

・タスクトレイ常駐型
・指定フォルダ内の画像を一定間隔で自動切り替え
・画像の追加や削除を検知して反映
・シャッフル表示可
・設定は config.json で管理
・軽量で低負荷

【対応画像形式】

jpg,jpeg,png,bmp


【動作環境】

・Windows 10 / Windows 11
・.NET Desktop Runtime 10（x64）

※ .NET Desktop Runtime 10（x64）は Microsoft 公式サイトからインストールできます  

https://dotnet.microsoft.com/download/dotnet/10.0

【使い方】

1. ZIP を展開します
2. config.json を編集して壁紙フォルダや表示間隔(秒)を設定します
3. WallpaperChanger.exe を実行します
4. タスクトレイにアイコンが表示されます
5. アイコンの右クリックメニューから終了できます

【config.json の例】（10秒ごとに"C:\test\Wallpapers"にある画像をshuffle表示)

{
  "folder": "C:\\test\\Wallpapers",
  "interval": 10,
  "shuffle": true
}

※jsonファイル内でのフォルダパスの表記には"\"の代わりに"\\"を使用してください

【ライセンス】

MIT License
