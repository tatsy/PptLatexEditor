PowerPoint LaTeX Editor (PptLatexEditor)
==========


インストール
-----
1. Download "anysizefont.sty" from CTAN

 このプラグインではanysizefontというスタイルファイルを使用しています．まず，以下のCTANのウェブサイトからanysizefont.styをダウンロードしてください．
 ([http://www.ctan.org/pkg/anyfontsize](http://www.ctan.org/pkg/anyfontsize)) and

 w32texをお使いの場合にはこの.styファイルを以下のディレクトリに入れてください。

"C:/w32tex/share/texmf-local/tex/latex".

2. Compile plugin

 もしVisual Studioをお使いの場合にはGithubから.slnファイルを落として、それを使ってプロジェクトをビルドしていただければインストールは完了です。

3. First use

 最初にお使いの際にはTexタブのSettingsボタンからplatex.exeのパスを指定してください。
 このプラグインではplatexとdvipngを使っていますのでplatex.exeのディレクトリにdvipng.exeがあることもご確認ください。
