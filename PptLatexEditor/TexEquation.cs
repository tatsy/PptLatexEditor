using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace PowerPointLatex
{
    class TexEquation
    {
        private static char[] trimableLetters = new char[] { '\n', '\r' };

        private static String outDir = @"C:\Users\Tatsuya\Desktop\pptlatex";
        private static String codeFileName = @"C:\Users\Tatsuya\Desktop\pptlatex\latexcode";

        public string TexCode { get; private set; }
        public Bitmap EqImage { get; private set; }

        public TexEquation()
        {
            TexCode = "";
            EqImage = null;
        }

        public void Render(String code, int fontSize, bool isFinal)
        {
            int renderFontSize = fontSize;
            if (isFinal)
            {
                renderFontSize = (int)(1.5 * renderFontSize);
            }

            StringWriter stream = new StringWriter();
            stream.WriteLine("% --PptLatexEditor--");
            stream.WriteLine("\\documentclass{article}");
            stream.WriteLine("\\usepackage{amsmath,amssymb}");
            stream.WriteLine("\\usepackage{anyfontsize}");
            stream.WriteLine("\\pagestyle{empty}");
            stream.WriteLine("\\begin{document}");
            stream.WriteLine("\\fontsize{" + renderFontSize.ToString() + "pt}{" + renderFontSize.ToString() + "pt}\\selectfont");
            stream.WriteLine("% --Font Size: " + fontSize.ToString() + "pt--");
            stream.WriteLine("\\begin{eqnarray*}");
            stream.WriteLine(code.Trim(trimableLetters));
            stream.WriteLine("\\end{eqnarray*}");
            stream.WriteLine("\\end{document}");
            stream.Close();
            TexCode = stream.ToString();

            String fileName = codeFileName;
            if (isFinal)
            {
                fileName += "_final";
            }

            StreamWriter writer = new StreamWriter(String.Format("{0}.tex", fileName));
            writer.Write(TexCode);
            writer.Close();

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.Arguments = @"/c C:\w32tex\bin\platex.exe -output-directory=" + outDir + " " + fileName + ".tex && "
                                     + @"C:\w32tex\bin\dvipng.exe -T tight --freetype0 -Q 5 -bd 1000 -o " + fileName + ".png " + fileName + ".dvi && /w";
            proc.Start();
            proc.WaitForExit(3000);
            proc.Close();

            if (File.Exists(fileName + ".png"))
            {
                Bitmap temp = (Bitmap)Image.FromFile(fileName + ".png");
                EqImage = new Bitmap(temp);
                temp.Dispose();
            }
        }

        public PowerPoint.Shape GetImageShape()
        {
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            return app.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(codeFileName + "_final.png", Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 100, 100);
        }
    }
}
