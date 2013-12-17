namespace PowerPointLatex
{
    partial class LatexCodeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.codeTextbox = new System.Windows.Forms.TextBox();
            this.equationBox = new System.Windows.Forms.PictureBox();
            this.previewButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.equationBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // codeTextbox
            // 
            this.codeTextbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.codeTextbox.Location = new System.Drawing.Point(0, 0);
            this.codeTextbox.Multiline = true;
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.Size = new System.Drawing.Size(572, 142);
            this.codeTextbox.TabIndex = 0;
            // 
            // equationBox
            // 
            this.equationBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.equationBox.Location = new System.Drawing.Point(0, 0);
            this.equationBox.Name = "equationBox";
            this.equationBox.Size = new System.Drawing.Size(572, 149);
            this.equationBox.TabIndex = 1;
            this.equationBox.TabStop = false;
            // 
            // previewButton
            // 
            this.previewButton.Location = new System.Drawing.Point(409, 149);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(75, 23);
            this.previewButton.TabIndex = 2;
            this.previewButton.Text = "Preview";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(490, 149);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numFontSize);
            this.panel1.Controls.Add(this.codeTextbox);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.previewButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(572, 183);
            this.panel1.TabIndex = 4;
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(13, 152);
            this.numFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(120, 19);
            this.numFontSize.TabIndex = 4;
            this.numFontSize.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // LatexCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 332);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.equationBox);
            this.Name = "LatexCodeForm";
            this.Text = "LatexCodeForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LatexCodeForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.equationBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox codeTextbox;
        private System.Windows.Forms.PictureBox equationBox;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numFontSize;
    }
}