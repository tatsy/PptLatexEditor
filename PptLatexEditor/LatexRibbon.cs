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
        private LatexCodeForm lcform;

        private void LatexRibbon_Load(object sender, RibbonUIEventArgs e)
        {            
            lcform = new LatexCodeForm();
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            lcform.Show();
        }
    }
}
