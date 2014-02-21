using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;

namespace PowerPointLatex
{
    public partial class LatexCodeForm : Form
    {
        String outDir = @"C:\Users\Tatsuya\Desktop\pptlatex";
        String codeFileName = @"C:\Users\Tatsuya\Desktop\pptlatex\latexcode";
        PowerPoint.Shapes shapes;
        PowerPoint.Shape targetShape;
        string texCode;
        Bitmap eqImage;

        public LatexCodeForm(PowerPoint.Shape shape = null)
        {
            InitializeComponent();
            shapes = Globals.PptLatexAddin.Application.ActiveWindow.Selection.SlideRange.Shapes;

            if (shape == null)
            {
                this.codeTextbox.Text = "";
            } 
            else
            {
                targetShape = shape;
                String texCode = shape.AlternativeText;
                String pattern = @"\\\[\r\n(.*)\r\n\\\]";
                MatchCollection match = Regex.Matches(texCode, pattern, RegexOptions.Singleline);
                if (match.Count >= 1)
                {
                    this.codeTextbox.Text = match[0].Groups[1].Value;
                }
            }
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            // コードとフォントサイズの取得
            String code = codeTextbox.Text;
            int fontSize = (int)numFontSize.Value;

            // コードをファイルに書き込む
            renderTexCode(code, fontSize);
            equationBox.Image = eqImage;
            equationBox.Location = new Point((equationBox.Parent.ClientSize.Width / 2) - (eqImage.Width / 2),
                              (equationBox.Parent.ClientSize.Height / 2) - (eqImage.Height / 2));
            equationBox.Refresh();
            this.Refresh();
        }

        // TeXのコードを画像としてレンダリング
        private void renderTexCode(string code, int fontSize)
        {
            StringWriter stream = new StringWriter();
            stream.WriteLine("% --PptLatexEditor--");
            stream.WriteLine("\\documentclass{article}");
            stream.WriteLine("\\usepackage{amsmath,amssymb}");
            stream.WriteLine("\\usepackage{anyfontsize}");
            stream.WriteLine("\\pagestyle{empty}");
            stream.WriteLine("\\begin{document}");
            stream.WriteLine("\\fontsize{" + fontSize.ToString() + "pt}{" + fontSize.ToString() + "pt}\\selectfont");
            stream.WriteLine("\\[");
            stream.WriteLine(code);
            stream.WriteLine("\\]");
            stream.WriteLine("\\end{document}");
            stream.Close();
            texCode = stream.ToString();

            StreamWriter writer = new StreamWriter(String.Format("{0}.tex", codeFileName));
            writer.Write(texCode);
            writer.Close();

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.Arguments = @"/c C:\w32tex\bin\platex.exe -output-directory=" + outDir + " " + codeFileName + ".tex && "
                                     + @"C:\w32tex\bin\dvipng.exe -T tight -Q 5 -bd 1000 -o " + codeFileName + ".png " + codeFileName + ".dvi && /w";
            proc.Start();
            proc.WaitForExit(3000);
            proc.Close();

            Image img = null;
            if(File.Exists(codeFileName + ".png"))
            {
                img = Image.FromFile(codeFileName + ".png");
                eqImage = new Bitmap(img);
                img.Dispose();
            }
            else
            {
                eqImage = null;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {            
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            if (eqImage != null)
            {
                try
                {
                    String code = codeTextbox.Text;
                    int fontSize = (int)numFontSize.Value;
                    renderTexCode(code, (int)(fontSize * 1.5));

                    PowerPoint.Shape picBox = app.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(codeFileName + ".png", Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 100, 100);
                    picBox.ScaleWidth(0.5f, Office.MsoTriState.msoTrue);
                    picBox.AlternativeText = texCode;
                    if (targetShape != null)
                    {
                        targetShape.Delete();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.Close();
        }

        private void LatexCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
