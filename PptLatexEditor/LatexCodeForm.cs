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

        private static string[] latexKeywords = {
            "int", "sum", "prod",
            "mathbf", "bf", 
            "rightarrow", "leftarrow", "Rightarrow", "Leftarrow",
            "hat", "tilde", "bar",
            "bigcap", "bigcup", "bigotimes", "bigoplus",
            "otimes", "oplus",  "times",
            "cases", "frac", "left", "right",
            "sqrt",
            "geq", "leq", "neq",
            "partial",
        };

        private static string[] environmentKeywords = {
            "begin", "end"                                                        
        };

        private static string[] greekKeywords = {
            "alpha", "beta", "gamma", "delta", "epsilon", "varepsilon", "zeta", "eta", "theta", "vartheta", "kappa", "lambda", "mu", "nu",
            "xi", "pi", "omega", "rho", "varrho", "sigma", "tau", "phi", "varphi", "kai", "psi"
        };

        private static char[] beginBlackets = { '(', '{', '[' };
        private static char[] endBlackets = { ')', '}', ']' };

        // Constructor
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
            // GET: code and fontsize
            String code = codeTextbox.Text;
            int fontSize = (int)numFontSize.Value;

            // save code for the file to be compiled
            renderTexCode(code, fontSize, false);
            int width  = texEq.EqImage.Width;
            int height = texEq.EqImage.Height;

            // shrink rendered image for display
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

        // render TeX code
        private void renderTexCode(string code, int fontSize, bool isFinal)
        {
            texEq.Render(code, fontSize, isFinal);
        }

        // OK button is clicked
        private void okButton_Click(object sender, EventArgs e)
        {            
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            //if (texEq.EqImage != null)
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
                        picBox.Top = targetShape.Top;
                        picBox.Left = targetShape.Left;
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

        // Close button is clicked
        private void LatexCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        // cursor changed in the code textbox
        private void codeTextbox_SelectionChanged(object sender, EventArgs e)
        {
            int currentSelectionStart = codeTextbox.SelectionStart;
            int currentSelectionLength = codeTextbox.SelectionLength;
            try
            {
                codeTextbox.SelectionChanged -= codeTextbox_SelectionChanged;
                string code = codeTextbox.Text;

                // initialize all the text
                codeTextbox.Select(0, code.Length);
                codeTextbox.SelectionColor = Color.Black;
                codeTextbox.SelectionBackColor = Color.White;


                // find blacket pair
                codeTextProcessBlacketPair(currentSelectionStart);
                codeTextProcessKeywords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                codeTextbox.Select(currentSelectionStart, currentSelectionLength);
                codeTextbox.SelectionChanged += codeTextbox_SelectionChanged;
            }
        }

        private void codeTextProcessBlacketPair(int currentPosition)
        {
            string code = codeTextbox.Text;
            for (int i = 0; i < beginBlackets.Length; i++)
            {
                int beginPosition = -1;
                int endPosition = -1;
                if (currentPosition > 0 && code[currentPosition - 1] == endBlackets[i])
                {
                    endPosition = currentPosition - 1;
                    int searchPosition = endPosition;
                    int countStack = 0;
                    do
                    {
                        if (code[searchPosition] == endBlackets[i]) countStack++;
                        if (code[searchPosition] == beginBlackets[i])
                        {
                            countStack--;
                            if (countStack == 0)
                            {
                                beginPosition = searchPosition;
                                break;
                            }
                        }
                    } while (--searchPosition >= 0);
                }

                if (currentPosition < code.Length - 1 && code[currentPosition] == beginBlackets[i])
                {
                    beginPosition = currentPosition;
                    int searchPosition = beginPosition;
                    int countStack = 0;
                    do
                    {
                        if (code[searchPosition] == beginBlackets[i]) countStack++;
                        if (code[searchPosition] == endBlackets[i])
                        {
                            countStack--;
                            if (countStack == 0)
                            {
                                endPosition = searchPosition;
                                break;
                            }
                        }
                    } while (++searchPosition < code.Length - 1);
                }

                if (beginPosition != -1 && endPosition != -1)
                {
                    codeTextbox.Select(beginPosition, 1);
                    codeTextbox.SelectionBackColor = Color.LightGray;
                    codeTextbox.Select(endPosition, 1);
                    codeTextbox.SelectionBackColor = Color.LightGray;
                    break;
                }
            }
        }

        private void codeTextProcessKeywords()
        {
            processKeywords(greekKeywords, Color.Gray);
            processKeywords(latexKeywords, Color.Blue);
            processKeywords(environmentKeywords, Color.MediumTurquoise);
        }

        private void processKeywords(string[] keywords, Color color)
        {
            string code = codeTextbox.Text;
            for (int i = 0; i < keywords.Length; i++)
            {
                Regex reg = new Regex(keywords[i]);
                foreach (Match match in reg.Matches(code))
                {
                    codeTextbox.Select(match.Index, keywords[i].Length);
                    codeTextbox.SelectionColor = color;
                }
            }
        }

        private void codeTextbox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
