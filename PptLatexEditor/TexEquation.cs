using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace PowerPointLatex
{
    enum RenderFormat
    {
        EPS,
        PDF,
        Image
    }

    static class TexEquation
    {
        private static char[] trimableLetters = new char[] { '\n', '\r' };

        private static String appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static String outDir = appDataDir + @"\pptlatex";
        private static String codeFileName = outDir + @"\latexcode";
        private static String configFile = outDir + @"\config.ini";

        public static string Render(String texCode, RenderFormat renderFormat = RenderFormat.Image)
        {

            String fileName = codeFileName;

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            using (StreamWriter writer = new StreamWriter(String.Format("{0}.tex", fileName)))
            {
                writer.Write(texCode);
            }

            return doRender(fileName, renderFormat);
        }

        /// <summary>
        /// Render specified latex file and return output filename
        /// </summary>
        /// <param name="fileName">LaTeX souce code base name</param>
        /// <returns>output image file such as JPEG, PNG, EPS, and PDF</returns>
        private static string doRender(string baseName, RenderFormat renderFormat)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            string outfile = "";
            try
            {
                proc.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = false;
                proc.StartInfo.RedirectStandardOutput = true;

                // get latex path
                StreamReader reader = new StreamReader(configFile);
                String latexPath = reader.ReadLine();
                reader.Close();

                String platexCmd = string.Format("{0} -output-directory={1} {2}.tex", latexPath, outDir, baseName);
                String dvipngCmd = string.Format("{0} -T tight --freetype0 -Q 5 -bd 1000 -o {1}.png {1}.dvi", Path.Combine(Path.GetDirectoryName(latexPath), "dvipng.exe"), baseName);
                String dvipsCmd  = string.Format("dvips -E -Ppdf {0}.dvi -o {0}.eps", baseName);
                String rungsCmd = string.Format("gswin32c -dSAFER -q -dBATCH -dNOPAUSE -sDEVICE=epswrite -dEPSCrop -r9600 -sOutputFile={0}_outline.eps {0}.eps", baseName);
                String ps2pdfCmd = string.Format("ps2pdf {0}_outline.eps {0}.pdf", baseName);

                proc.StartInfo.Arguments = @"/c " + platexCmd;
                if (renderFormat == RenderFormat.Image)
                {
                    proc.StartInfo.Arguments += @" && " + dvipngCmd;
                    outfile = baseName + ".png";
                }
                else if (renderFormat == RenderFormat.EPS)
                {
                    proc.StartInfo.Arguments += @" && " + dvipsCmd;
                    proc.StartInfo.Arguments += @" && " + rungsCmd;
                    outfile = baseName + "_outline.eps";
                }

                else if (renderFormat == RenderFormat.PDF)
                {
                    proc.StartInfo.Arguments += @" && " + dvipsCmd;
                    proc.StartInfo.Arguments += @" && " + rungsCmd;
                    proc.StartInfo.Arguments += @" && " + ps2pdfCmd;
                    outfile = baseName + ".pdf";
                }
                proc.Start();
                checkLatexError(proc.StandardOutput);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                proc.WaitForExit(3000);
                proc.Close();
            }
            return outfile;
        }

        public static PowerPoint.Shape GetImageShape()
        {
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            return app.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(codeFileName + ".png", Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 100, 100);
        }

        public static string WrapLatexEquationCode(string code, int fontSize, Color textColor, bool isFinal)
        {
            int renderFontSize = fontSize;
            if (isFinal)
            {
                renderFontSize = (int)(1.5 * renderFontSize);
            }

            String colorDesc = String.Format("{0}, {1}, {2}", textColor.R / 255.0, textColor.G / 255.0, textColor.B / 255);

            StringWriter stream = new StringWriter();
            stream.WriteLine("% --PptLatexEditor--");
            stream.WriteLine("\\documentclass{article}");
            stream.WriteLine("\\usepackage{amsmath,amssymb}");
            stream.WriteLine("\\usepackage{xcolor}");
            stream.WriteLine("\\usepackage{anyfontsize}");
            stream.WriteLine("\\pagestyle{empty}");
            stream.WriteLine("\\definecolor{mycolor}{rgb}{" + colorDesc + "}");
            stream.WriteLine("\\begin{document}");
            stream.WriteLine("\\fontsize{" + renderFontSize.ToString() + "pt}{" + renderFontSize.ToString() + "pt}\\selectfont");
            stream.WriteLine("% --Font Size: " + fontSize.ToString() + "pt--");
            stream.WriteLine("\\textcolor{mycolor}{");
            stream.WriteLine("\\begin{eqnarray*}");
            stream.WriteLine(code.Trim(trimableLetters));
            stream.WriteLine("\\end{eqnarray*}");
            stream.WriteLine("}");
            stream.WriteLine("\\end{document}");
            stream.Close();
            return stream.ToString();
        }

        private static void checkLatexError(StreamReader stdOutputStream)
        {
            string line;
            while ((line = stdOutputStream.ReadLine()) != null)
            {
                if (line.Contains("Emergency stop"))
                {
                    throw new Exception("Compilation failed !!");
                }
            }
        }
    }
}
