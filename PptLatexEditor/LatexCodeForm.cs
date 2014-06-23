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
        private PowerPoint.Shapes shapes;
        private PowerPoint.Shape targetShape;
        private TexEquation texEq;

        // フォームのコンストラクタ
        public LatexCodeForm(PowerPoint.Shape shape = null)
        {
            InitializeComponent();
            shapes = Globals.PptLatexAddin.Application.ActiveWindow.Selection.SlideRange.Shapes;
            texEq  = new TexEquation();

            if (shape == null)
            {
                this.codeTextbox.Text = "";
            } 
            else
            {
                targetShape = shape;
                String texCode = shape.AlternativeText;
                String patCode = @"\\begin\{eqnarray\*\}[\r\n]{0,2}(.*)[\r\n]{0,2}\\end\{eqnarray\*\}";
                String patSize = "% --Font Size: ([0-9]+)pt--";
                MatchCollection matCode = Regex.Matches(texCode, patCode, RegexOptions.Singleline);
                if (matCode.Count >= 1)
                {
                    this.codeTextbox.Text = matCode[0].Groups[1].Value;
                }
                else
                {
                    this.codeTextbox.Text = "";
                }

                MatchCollection matSize = Regex.Matches(texCode, patSize, RegexOptions.Singleline);
                if (matSize.Count >= 1)
                {
                    this.numFontSize.Value = Int32.Parse(matSize[0].Groups[1].Value);
                }
                else
                {
                    this.numFontSize.Value = 30;
                }
            }
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            // コードとフォントサイズの取得
            String code = codeTextbox.Text;
            int fontSize = (int)numFontSize.Value;

            // コードをファイルに書き込む
            renderTexCode(code, fontSize, false);
            int width  = texEq.EqImage.Width;
            int height = texEq.EqImage.Height;

            // 画像が大きければ縮小する
            if (width > equationBox.Width || height > equationBox.Height)
            {
                double scaleW = Math.Min(1.0, equationBox.Width  / (double)width);
                double scaleH = Math.Min(1.0, equationBox.Height / (double)height);
                double scale = Math.Min(scaleW, scaleH) * 0.9;
                width  = (int)(width  * scale);
                height = (int)(height * scale);
            }
            equationBox.Image = new Bitmap(texEq.EqImage, width, height);
            equationBox.Location = new Point((equationBox.Parent.ClientSize.Width / 2) - (width / 2),
                              (equationBox.Parent.ClientSize.Height / 2) - (height / 2));
            equationBox.Refresh();
            this.Refresh();
        }

        // TeXのコードを画像としてレンダリング
        private void renderTexCode(string code, int fontSize, bool isFinal)
        {
            texEq.Render(code, fontSize, isFinal);
        }

        // OKボタンがクリックされた
        private void okButton_Click(object sender, EventArgs e)
        {            
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            if (texEq.EqImage != null)
            {
                try
                {
                    String code = codeTextbox.Text;
                    int fontSize = (int)numFontSize.Value;
                    renderTexCode(code, fontSize, true);

                    PowerPoint.Shape picBox = texEq.GetImageShape();
                    picBox.ScaleWidth(0.5f, Office.MsoTriState.msoTrue);
                    picBox.AlternativeText = texEq.TexCode;
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

        // 閉じるボタンがクリックされた
        private void LatexCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
