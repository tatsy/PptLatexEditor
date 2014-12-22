PowerPoint LaTeX Editor (PptLatexEditor)
==========
This project provides a plug-in for Microsoft PowerPoint that enables to insert LaTeX rendered
equation images from TeX codes. The interface is very simple and provides only equation generation
and equation edit.

Installation
-----
1. Download "anysizefont.sty" from CTAN

 This plug-in employs "anysizefont.sty" to control font size of rendered equation images.
 So please download anysizefont.sty from CTAN ([http://www.ctan.org/pkg/anyfontsize](http://www.ctan.org/pkg/anyfontsize)) and
 move it to your latex style folder. For w32tex users, the style file folder is like
 "C:/w32tex/share/texmf-local/tex/latex".

2. Compile plugin

 For Visual Studio users, only opening solution file and compiling the project finish the installation.
 I'm now planning to provide installation file at the other webpage, so please wait for a moment
 for if you are a non VS user.

3. Initial setting

 This plug-in uses "platex.exe" and "dvipng.exe". Therefore, please specify the location of these files using the "Settings" button in the "TeX" tab.
    
 Thanks!
