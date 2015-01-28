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

        private bool isSelectBackwardly = false;
        int currentSelectionStart = 0;
        int currentSelectionLength = 0;
        
        private static Regex latexKeywordPattern;
        private static Regex environmentKeywordPattern;
        private static Regex greekKeywordPattern;

        private static string[] latexKeywords = {
            "int", "oint", "sum", "prod", "min", "max", "lim", "infty", "ell",
            "bf", "rm", "mathbf", "mathcal", "mathbb", "mbox", "text", "textrm",
            "rightarrow", "leftarrow", "Rightarrow", "Leftarrow",
            "hat", "tilde", "bar", 
            "bigcap", "bigcup", "bigotimes", "bigoplus",
            "otimes", "oplus",  "times", "pm", "mp", 
            "frac", "cfrac", "left", "right",
            "sqrt",
            "ll", "gg", "geq", "leq", "neq","approx", "sim", "in", "equiv",
            "partial",
            "quad", "qquad"
        };

        private static string[] environmentKeywords = {
            "begin", "end"                                                        
        };

        private static string[] greekKeywords = {
            "alpha", "beta", "gamma", "Gamma", "delta", "Delta", "epsilon", "varepsilon", "zeta", "eta", "theta", "Theta", "vartheta", "kappa", "lambda", "Lambda", "mu", "nu",
            "xi", "pi", "omega", "rho", "varrho", "sigma", "Sigma", "tau", "phi", "Phi", "varphi", "kai", "psi", "Psi"
        };

        private static char[] beginBlackets = { '(', '{', '[' };
        private static char[] endBlackets = { ')', '}', ']' };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shape">Target shape is input if it is selected</param>
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
                String patCode = @"\\begin\{eqnarray\*\}[\r\n]{0,2}(.*)[\r\n]{0,2}\\end\{eqnarray\*\}";
                String patSize = "% --Font Size: ([0-9]+)pt--";
                MatchCollection matCode = Regex.Matches(texCode, patCode, RegexOptions.Singleline);
                if (matCode.Count >= 1)
                {
                    this.codeTextbox.Text = matCode[0].Groups[1].Value;
                    this.codeTextbox_TextChanged(this, new EventArgs());
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

        /// <summary>
        /// Static constructor
        /// </summary>
        static LatexCodeForm()
        {
            LatexCodeForm.compileKeywords(latexKeywords, out latexKeywordPattern);
            LatexCodeForm.compileKeywords(environmentKeywords, out environmentKeywordPattern);
            LatexCodeForm.compileKeywords(greekKeywords, out greekKeywordPattern);
        }

        /// <summary>
        /// Convert keyword array to OR regex pattern
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="regex"></param>
        private static void compileKeywords(string[] keywords, out Regex regex)
        {
            string pattern = "";
            for (int i = 0; i < keywords.Length; i++)
            {
                pattern += "\\\\" + keywords[i];
                if (i != keywords.Length - 1)
                {
                    pattern += "[^0-9a-zA-Z]|";
                }
            }
            regex = new Regex(pattern);
        }

        /// <summary>
        /// Event function: preview button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previewButton_Click(object sender, EventArgs e)
        {
            // GET: code and fontsize
            String code = codeTextbox.Text;
            int fontSize = (int)numFontSize.Value;

            // save code for the file to be compiled
            Bitmap eqImage = null;
            try
            {
                code = TexEquation.WrapLatexEquationCode(code, fontSize, false);
                eqImage = renderTexCode(code);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            int width  = eqImage.Width;
            int height = eqImage.Height;

            // shrink rendered image for display
            if (width > equationBox.Width || height > equationBox.Height)
            {
                double scaleW = Math.Min(1.0, equationBox.Width  / (double)width);
                double scaleH = Math.Min(1.0, equationBox.Height / (double)height);
                double scale = Math.Min(scaleW, scaleH) * 0.9;
                width  = (int)(width  * scale);
                height = (int)(height * scale);
            }

            equationBox.Image = new Bitmap(eqImage, width, height);
            equationBox.Location = new Point((equationBox.Parent.ClientSize.Width / 2) - (width / 2),
                              (equationBox.Parent.ClientSize.Height / 2) - (height / 2));
            equationBox.Refresh();
            this.Refresh();
        }

        // render TeX code
        private Bitmap renderTexCode(string code)
        {
            try
            {
                string outfile = TexEquation.Render(code);
                Image temp = Image.FromFile(outfile);
                Bitmap ret = new Bitmap(temp);
                temp.Dispose();
                return ret;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw e;
            }
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
                    code = TexEquation.WrapLatexEquationCode(code, fontSize, true);
                    renderTexCode(code);

                    PowerPoint.Shape picBox = TexEquation.GetImageShape();
                    picBox.ScaleWidth(0.5f, Office.MsoTriState.msoTrue);
                                        
                    picBox.AlternativeText = code;
                    if (targetShape != null)
                    {
                        picBox.Top = targetShape.Top;
                        picBox.Left = targetShape.Left;
                        targetShape.Delete();
                    }
                }
                catch
                {
                    return;
                }
                finally
                {
                    this.Close();
                }
            }
        }

        // Close button is clicked
        private void LatexCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        // cursor changed in the code textbox
        private void codeTextbox_SelectionChanged(object sender, EventArgs e)
        {
            if (isSelectBackwardly)
            {
                currentSelectionStart = Math.Max(0, currentSelectionStart - 1);
                currentSelectionLength = currentSelectionLength + 1;
            }
            else
            {
                currentSelectionStart = codeTextbox.SelectionStart;
                currentSelectionLength = codeTextbox.SelectionLength;
            }
            syntaxHighlight();
        }

        private void codeTextbox_TextChanged(object sender, EventArgs e)
        {
            syntaxHighlight(true);
        }

        private void syntaxHighlight(bool isUpdateKeyword = false)
        {
            try
            {
                codeTextbox.SelectionChanged -= codeTextbox_SelectionChanged;
                codeTextbox.TextChanged -= codeTextbox_TextChanged;

                string code = codeTextbox.Text;

                // initialize all the text
                codeTextbox.Select(0, code.Length);
                codeTextbox.SelectionBackColor = Color.White;
                if (isUpdateKeyword)
                {
                    codeTextbox.SelectionColor = Color.Black;
                }

                // find blacket pair
                codeTextProcessBlacketPair(currentSelectionStart);
                if (isUpdateKeyword)
                {
                    codeTextProcessKeywords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                codeTextbox.Select(currentSelectionStart, currentSelectionLength);
                codeTextbox.SelectionChanged += codeTextbox_SelectionChanged;
                codeTextbox.TextChanged += codeTextbox_TextChanged;
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
            processKeywords(greekKeywordPattern, Color.Gray);
            processKeywords(latexKeywordPattern, Color.Blue);
            processKeywords(environmentKeywordPattern, Color.MediumTurquoise);
        }

        private void processKeywords(Regex regex, Color color)
        {
            string code = codeTextbox.Text;
            foreach (Match match in regex.Matches(code))
            {
                codeTextbox.Select(match.Index, match.Length);
                codeTextbox.SelectionColor = color;
            }
        }
    }
}
