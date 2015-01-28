using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;

namespace PowerPointLatex
{
    public partial class LatexRibbon
    {
        private static string identifiableLatexComment = "--PptLatexEditor--";
        private SaveFileDialog saveFileDialog;

        private void LatexRibbon_Load(object sender, RibbonUIEventArgs e)
        {            
            saveFileDialog = new SaveFileDialog();            
        }


#region mouse operations

        /// <summary>
        /// Click "Equation" button to create a new equation
        /// </summary>
        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            LatexCodeForm lcform = new LatexCodeForm();
            lcform.Show();
        }

        /// <summary>
        /// Click "Edit" button to edit an existing equation
        /// </summary>
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            PowerPoint.Selection select = app.ActiveWindow.Selection;
            if (select.Type == PowerPoint.PpSelectionType.ppSelectionShapes)
            {
                if (countLatexImages(select.ShapeRange) == 1)
                {
                    foreach (PowerPoint.Shape shape in select.ShapeRange)
                    {
                        if (shape.AlternativeText.Contains(identifiableLatexComment))
                        {
                            LatexCodeForm lcform = new LatexCodeForm(shape);
                            lcform.Show();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Click "Setting" button
        /// </summary>
        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            SettingForm setForm = new SettingForm();
            setForm.Show();
        }

        /// <summary>
        /// Click "Save as button"
        /// </summary>
        private void SaveAsButton_Click(object sender, RibbonControlEventArgs e)
        {
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            PowerPoint.Selection select = app.ActiveWindow.Selection;
            if(select.Type == PowerPoint.PpSelectionType.ppSelectionShapes)
            {
                if (countLatexImages(select.ShapeRange) == 1)
                {
                    foreach (PowerPoint.Shape shape in select.ShapeRange)
                    {
                        if (shape.AlternativeText.Contains(identifiableLatexComment))
                        {
                            saveFileDialog.Filter = "EPS (*.eps)|*.eps|"
                                                  + "PDF (*.pdf)|*.pdf|"
                                                  + "Image Files(*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                            saveFileDialog.FileName = "default.eps";
                            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                string targetFile = saveFileDialog.FileName;
                                RenderFormat renderFormat = (RenderFormat)(saveFileDialog.FilterIndex - 1);
                                try
                                {
                                    String outfile = TexEquation.Render(shape.AlternativeText, renderFormat);
                                    if (File.Exists(targetFile))
                                    {
                                        File.Delete(targetFile);
                                    }

                                    if (renderFormat != RenderFormat.Image)
                                    {
                                        File.Move(outfile, saveFileDialog.FileName);
                                    }
                                    else
                                    {
                                        MessageBox.Show(outfile);
                                        Image img = Image.FromFile(outfile);
                                        img.Save(targetFile);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("[Save as] " + ex.Message + "\n" + ex.StackTrace);
                                }
                            }
                        }
                    }
                }
            }
        }

#endregion

        private int countLatexImages(PowerPoint.ShapeRange shapeRange)
        {
            int ret = 0;
            foreach (PowerPoint.Shape shape in shapeRange)
            {
                if (shape.AlternativeText.Contains(identifiableLatexComment))
                {
                    ret++;
                }
            }
            return ret;
        }
    }
}
