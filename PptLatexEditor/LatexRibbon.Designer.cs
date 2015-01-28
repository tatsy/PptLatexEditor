namespace PowerPointLatex
{
    partial class LatexRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// デザイナー変数が必要です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public LatexRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナーのサポートに必要なメソッドです。
        /// このメソッドの内容をコード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabTexEdit = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.button3 = this.Factory.CreateRibbonButton();
            this.SaveAsButton = this.Factory.CreateRibbonButton();
            this.tabTexEdit.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            // 
            // tabTexEdit
            // 
            this.tabTexEdit.Groups.Add(this.group1);
            this.tabTexEdit.Groups.Add(this.group2);
            this.tabTexEdit.Label = "TeX";
            this.tabTexEdit.Name = "tabTexEdit";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button1);
            this.group1.Items.Add(this.button2);
            this.group1.Name = "group1";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Label = "Equation";
            this.button1.Name = "button1";
            this.button1.OfficeImageId = "FunctionWizard";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Label = "Edit";
            this.button2.Name = "button2";
            this.button2.OfficeImageId = "SignatureLineInsert";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.button3);
            this.group2.Items.Add(this.SaveAsButton);
            this.group2.Name = "group2";
            // 
            // button3
            // 
            this.button3.Label = "Setting";
            this.button3.Name = "button3";
            this.button3.OfficeImageId = "SearchTools";
            this.button3.ShowImage = true;
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Label = "Save as";
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.OfficeImageId = "AutoSigInsertPictureFromFile";
            this.SaveAsButton.ShowImage = true;
            this.SaveAsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SaveAsButton_Click);
            // 
            // LatexRibbon
            // 
            this.Name = "LatexRibbon";
            this.RibbonType = "Microsoft.PowerPoint.Presentation";
            this.Tabs.Add(this.tabTexEdit);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.LatexRibbon_Load);
            this.tabTexEdit.ResumeLayout(false);
            this.tabTexEdit.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabTexEdit;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SaveAsButton;
    }

    partial class ThisRibbonCollection
    {
        internal LatexRibbon LatexRibbon
        {
            get { return this.GetRibbon<LatexRibbon>(); }
        }
    }
}
