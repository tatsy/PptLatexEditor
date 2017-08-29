using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
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

        int currentSelectionStart = 0;
        int currentSelectionLength = 0;
        bool currentSelectionL2R = true;

        private static TrieTree latexKeywordTrie;
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
                String patColor = @"\\definecolor\{mycolor\}\{rgb\}\{([0-9\.,\s]+)\}";
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

                MatchCollection matColor = Regex.Matches(texCode, patColor, RegexOptions.Singleline);
                if (matColor.Count >= 1)
                {
                    String[] items = Regex.Split(matColor[0].Groups[1].Value, ",");
                    int r = Math.Max(0, Math.Min((int)(255.0 * Double.Parse(items[0])), 255));
                    int g = Math.Max(0, Math.Min((int)(255.0 * Double.Parse(items[1])), 255));
                    int b = Math.Max(0, Math.Min((int)(255.0 * Double.Parse(items[2])), 255));
                    this.colorBox.BackColor = Color.FromArgb(r, g, b);
                }
                else
                {
                    this.colorBox.BackColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Static constructor
        /// </summary>
        static LatexCodeForm()
        {
            List<String> keywords = new List<String>();
            foreach (String s in Regex.Split(Properties.Resources.LatexKeywords, "\n"))
            {
                if (s.Length > 0)
                {
                    keywords.Add(s.Trim());
                }
            }
            latexKeywordTrie = new TrieTree(keywords);
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
                code = TexEquation.WrapLatexEquationCode(code, fontSize, colorBox.BackColor, false);
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
            //equationBox.Location = new Point((equationBox.Parent.ClientSize.Width / 2) - (width / 2),
            //                  (equationBox.Parent.ClientSize.Height / 2) - (height / 2));
            //equationBox.Refresh();
            this.Refresh();
        }

        // render TeX code
        private Bitmap renderTexCode(string code)
        {
            int trials = 10;
            int renderTimeout = 500;
            try
            {
                string outfile = null;
                Thread thread = new Thread(new ThreadStart( () => {
                        outfile = TexEquation.Render(code);
                }));

                thread.Start();
                for (int i = 0; i < trials; i++)
                {
                    Thread.Sleep(renderTimeout);
                    if (outfile != null)
                    {
                        Image temp = Image.FromFile(outfile);
                        Bitmap ret = new Bitmap(temp);
                        temp.Dispose();
                        return ret;
                    }
                }

                thread.Abort();
                MessageBox.Show("Compilation failed!!");
                return null;
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
                    code = TexEquation.WrapLatexEquationCode(code, fontSize, colorBox.BackColor, true);
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
            currentSelectionL2R = (currentSelectionStart == codeTextbox.SelectionStart);
            currentSelectionStart = codeTextbox.SelectionStart;
            currentSelectionLength = codeTextbox.SelectionLength;
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
                if (currentSelectionL2R)
                {
                    codeTextbox.Select(currentSelectionStart, currentSelectionLength);
                }
                else
                {
                    codeTextbox.Select(currentSelectionStart + currentSelectionLength, -currentSelectionLength);
                }
                
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
            String code = codeTextbox.Text;
            int codeSize = code.Length;
            for (int i = 0; i < codeSize; i++)
            {
                if (i == 0 || !Char.IsLetter(code[i - 1]))
                {
                    for (int j = i; j < codeSize; j++)
                    {
                        if (j == codeSize - 1 || !Char.IsLetter(code[j + 1]))
                        {
                            String subCode = code.Substring(i, j - i + 1);
                            if (latexKeywordTrie.Contain(subCode))
                            {
                                codeTextbox.Select(i, j - i + 1);
                                codeTextbox.SelectionColor = Color.Gray;
                            }
                        }
                    }
                }
            }
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

        private void colorBox_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorBox.BackColor = colorDialog1.Color;
            }
        }

        private void equationBox_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(SystemColors.Control);
            Graphics g = e.Graphics;
            g.FillRectangle(brush, 0, 0, equationBox.Width, equationBox.Height);

            Image image = equationBox.Image;
            if (equationBox.Image != null)
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image,
                    equationBox.Width / 2 - image.Width / 2,  
                    equationBox.Height / 2 - image.Height / 2,
                    image.Width,
                    image.Height);
            }
        }
    }
}
