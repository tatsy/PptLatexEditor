using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        string texCode;
        Bitmap eqImage;

        public LatexCodeForm()
        {
            InitializeComponent();
            shapes = Globals.PptLatexAddin.Application.ActiveWindow.Selection.SlideRange.Shapes;
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            // コードとフォントサイズの取得
            String code = codeTextbox.Text;
            int fontSize = (int)numFontSize.Value;

            // コードをファイルに書き込む
            renderTexCode(code, fontSize);
            equationBox.Image = new Bitmap(eqImage);
            this.Refresh();
        }

        // TeXのコードを画像としてレンダリング
        private void renderTexCode(string code, int fontSize)
        {
            StringWriter stream = new StringWriter();
            stream.WriteLine("% --PptLatexEditor--");
            stream.WriteLine("\\documentclass{article}");
            stream.WriteLine("\\usepackage{amsmath,amssymb}");
            stream.WriteLine("\\pagestyle{empty}");
            stream.WriteLine("\\begin{document}");
            stream.WriteLine("\\fontsize{" + fontSize.ToString() + "}{3pt}\\selectfont");
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
            //proc.StartInfo.CreateNoWindow = true;
            //proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.Arguments = @"/c C:\w32tex\bin\platex.exe -output-directory=" + outDir + " " + codeFileName + ".tex && "
                                     + @"C:\w32tex\bin\dvipng.exe -T tight -Q 5 -bd 1000 -o " + codeFileName + ".png " + codeFileName + ".dvi && /w";
            proc.Start();
            proc.WaitForExit();
            proc.Close();

            Image img = Image.FromFile(codeFileName + ".png");
            eqImage = new Bitmap(img);
        }

        private void okButton_Click(object sender, EventArgs e)
        {            
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            try
            {
                PowerPoint.Shape picBox = app.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(codeFileName + ".png", Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 100, 100);
                picBox.AlternativeText = texCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void LatexCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
