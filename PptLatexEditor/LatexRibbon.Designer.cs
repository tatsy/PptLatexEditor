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
            this.tabTexEdit.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tabTexEdit
            // 
            this.tabTexEdit.Groups.Add(this.group1);
            this.tabTexEdit.Label = "TeX";
            this.tabTexEdit.Name = "tabTexEdit";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button1);
            this.group1.Name = "group1";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Image = global::PowerPointLatex.Properties.Resources.pptlatex;
            this.button1.Label = "Equation";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
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

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabTexEdit;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection
    {
        internal LatexRibbon LatexRibbon
        {
            get { return this.GetRibbon<LatexRibbon>(); }
        }
    }
}
