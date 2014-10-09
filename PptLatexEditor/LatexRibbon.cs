using System;
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
        private void LatexRibbon_Load(object sender, RibbonUIEventArgs e)
        {            
            
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            LatexCodeForm lcform = new LatexCodeForm();
            lcform.Show();
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            PowerPoint.Application app = Globals.PptLatexAddin.Application;
            PowerPoint.Selection sel = app.ActiveWindow.Selection;
            if (sel.Type == PowerPoint.PpSelectionType.ppSelectionShapes)
            {
                foreach (PowerPoint.Shape shape in sel.ShapeRange)
                {
                    if (shape.AlternativeText.Contains("--PptLatexEditor--"))
                    {
                        LatexCodeForm lcform = new LatexCodeForm(shape);
                        lcform.Show();
                    }
                }
            }
        }

        // Click "Setting" button
        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            SettingForm setForm = new SettingForm();
            setForm.Show();
        }
    }
}
