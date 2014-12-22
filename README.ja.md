PowerPoint LaTeX Editor (PptLatexEditor)
==========
このプラグインはLaTeXのコードをコンパイルして、数式の画像をスライドに挿入するプラグインです。

インストール
-----
1. スタイルファイル "anysizefont.sty" のダウンロード

 このプラグインではanysizefontというスタイルファイルを使用しています．まず，以下のCTANのウェブサイトからanysizefont.styをダウンロードしてください．
 ([http://www.ctan.org/pkg/anyfontsize](http://www.ctan.org/pkg/anyfontsize)) and

 w32texをお使いの場合にはこの.styファイルを以下のディレクトリに入れてください。

 "C:/w32tex/share/texmf-local/tex/latex".

2. プラグインのビルド

 もしVisual Studioをお使いの場合にはGithubから.slnファイルを落として、それを使ってプロジェクトをビルドしていただければインストールは完了です。

3. 初期設定

 最初にお使いの際にはTexタブのSettingsボタンからplatex.exeのパスを指定してください。
 このプラグインではplatexとdvipngを使っていますのでplatex.exeのディレクトリにdvipng.exeがあることもご確認ください。
