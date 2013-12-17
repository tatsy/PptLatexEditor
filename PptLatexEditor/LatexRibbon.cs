using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
