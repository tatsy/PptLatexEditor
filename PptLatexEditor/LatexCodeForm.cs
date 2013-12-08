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
        private PowerPoint.Slide slide;
        String codeFileName = @"C:\Users\Tatsuya\Desktop\pptlatex";

        public LatexCodeForm()
        {
            InitializeComponent();
            this.slide = slide;
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            String code = codeTextbox.Text;
            int    fontSize = (int)numFontSize.Value;

            // コードをファイルに書き込む
            String outDir       = @"C:\Users\Tatsuya\Desktop";
            String codeFileName = @"C:\Users\Tatsuya\Desktop\pptlatex";
            StreamWriter writer = new StreamWriter(codeFileName + ".tex");
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
                int nslides = app.ActivePresentation.Slides.Count;
                app.ActiveWindow.Selection.SlideRange.Shapes.AddPicture(codeFileName + ".png", Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 100, 100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hide();
        }

        private void LatexCodeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                return;
            }
            this.Close();
        }
    }
}
