using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools;

namespace PowerPointLatex
{
    public partial class PptLatexAddin
    {
        public static SortedSet<int> texIdDictionary;
 
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            texIdDictionary = new SortedSet<int>();
            Application.WindowSelectionChange += new PowerPoint.EApplication_WindowSelectionChangeEventHandler(Application_WindowSelectionChange);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void Application_WindowSelectionChange(PowerPoint.Selection sel)
        {
            if (sel.Type == PowerPoint.PpSelectionType.ppSelectionShapes)
            {
                //if(texIdDictionary.Contains(sel.ShapeRange.Id))
                //{
                    MessageBox.Show(sel.ShapeRange.Id.ToString());
                //}
            }
        }
        
        #region VSTO で生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
