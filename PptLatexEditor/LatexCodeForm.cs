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

        public LatexCodeForm()
        {
            InitializeComponent();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            String code = codeTextbox.Text;
            int    fontSize = (int)numFontSize.Value;

            // コードをファイルに書き込む
            StreamWriter writer = new StreamWriter(String.Format("{0}-{1}.tex", codeFileName, 0));
            writer.WriteLine("\\documentclass{article}");
            writer.WriteLine("\\usepackage{amsmath,amssymb}");
            writer.WriteLine("\\pagestyle{empty}");
            writer.WriteLine("\\begin{document}");
            writer.WriteLine("\\fontsize{" + fontSize.ToString() + "}{3pt}\\selectfont");
            writer.WriteLine("\\[");
            writer.WriteLine(code);
            writer.WriteLine("\\]");
            writer.WriteLine("\\end{document}");
            writer.Close();

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            proc.StartInfo.CreateNoWindow = true;

            proc.StartInfo.Arguments = @"/c C:\w32tex\bin\platex.exe -output-directory=" + outDir + " " + codeFileName + ".tex && "
                                     + @"C:\w32tex\bin\dvipng.exe -T tight -Q 5 -bd 1000 -o " + codeFileName + ".png " + codeFileName + ".dvi && /w";
            proc.Start();
            proc.WaitForExit();
            proc.Close();

            Image img = Image.FromFile(codeFileName + ".png");
            equationBox.Image = new Bitmap(img);
            img.Dispose();
            this.Refresh();
        }

        private void okButton_Click(object sender, EventArgs e)
        {            
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            try
            {
                app.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(codeFileName + ".png", Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 100, 100);
                MessageBox.Show(app.ActiveWindow.Selection.SlideRange.Shapes.Count.ToString());
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
