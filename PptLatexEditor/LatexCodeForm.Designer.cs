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
            this.panel2 = new System.Windows.Forms.Panel();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.equationBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // codeTextbox
            // 
            this.codeTextbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextbox.Location = new System.Drawing.Point(16, 254);
            this.codeTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.codeTextbox.Multiline = true;
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.Size = new System.Drawing.Size(729, 112);
            this.codeTextbox.TabIndex = 0;
            // 
            // equationBox
            // 
            this.equationBox.Location = new System.Drawing.Point(16, 15);
            this.equationBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.equationBox.Name = "equationBox";
            this.equationBox.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.equationBox.Size = new System.Drawing.Size(731, 228);
            this.equationBox.TabIndex = 1;
            this.equationBox.TabStop = false;
            // 
            // previewButton
            // 
            this.previewButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewButton.Location = new System.Drawing.Point(533, 374);
            this.previewButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(100, 29);
            this.previewButton.TabIndex = 2;
            this.previewButton.Text = "Preview";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(641, 374);
            this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 29);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.numFontSize);
            this.panel1.Controls.Add(this.codeTextbox);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.previewButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 415);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.equationBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(763, 246);
            this.panel2.TabIndex = 5;
            // 
            // numFontSize
            // 
            this.numFontSize.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFontSize.Location = new System.Drawing.Point(29, 378);
            this.numFontSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(81, 25);
            this.numFontSize.TabIndex = 1;
            this.numFontSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // LatexCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 415);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LatexCodeForm";
            this.Text = "Input Your Equation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LatexCodeForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.equationBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel2;
    }
}