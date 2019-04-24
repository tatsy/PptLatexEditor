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
            this.equationBox = new System.Windows.Forms.PictureBox();
            this.previewButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorBox = new System.Windows.Forms.Button();
            this.codeTextbox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.equationBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // equationBox
            // 
            this.equationBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equationBox.Location = new System.Drawing.Point(0, 0);
            this.equationBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.equationBox.Name = "equationBox";
            this.equationBox.Padding = new System.Windows.Forms.Padding(8);
            this.equationBox.Size = new System.Drawing.Size(950, 296);
            this.equationBox.TabIndex = 1;
            this.equationBox.TabStop = false;
            this.equationBox.Paint += new System.Windows.Forms.PaintEventHandler(this.equationBox_Paint);
            // 
            // previewButton
            // 
            this.previewButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previewButton.Location = new System.Drawing.Point(667, 448);
            this.previewButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(125, 34);
            this.previewButton.TabIndex = 2;
            this.previewButton.Text = "Preview";
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(802, 448);
            this.okButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(125, 34);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.colorBox);
            this.panel1.Controls.Add(this.codeTextbox);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.numFontSize);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.previewButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 495);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(228, 458);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(27, 458);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Size";
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.SystemColors.WindowText;
            this.colorBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.colorBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.colorBox.Location = new System.Drawing.Point(300, 450);
            this.colorBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(45, 34);
            this.colorBox.TabIndex = 7;
            this.colorBox.UseVisualStyleBackColor = false;
            this.colorBox.Click += new System.EventHandler(this.colorBox_Click);
            // 
            // codeTextbox
            // 
            this.codeTextbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextbox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.codeTextbox.Location = new System.Drawing.Point(20, 300);
            this.codeTextbox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.codeTextbox.Name = "codeTextbox";
            this.codeTextbox.Size = new System.Drawing.Size(911, 142);
            this.codeTextbox.TabIndex = 0;
            this.codeTextbox.Text = "";
            this.codeTextbox.SelectionChanged += new System.EventHandler(this.codeTextbox_SelectionChanged);
            this.codeTextbox.TextChanged += new System.EventHandler(this.codeTextbox_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.equationBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(950, 296);
            this.panel2.TabIndex = 5;
            // 
            // numFontSize
            // 
            this.numFontSize.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFontSize.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numFontSize.Location = new System.Drawing.Point(87, 452);
            this.numFontSize.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.numFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(102, 29);
            this.numFontSize.TabIndex = 1;
            this.numFontSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // LatexCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 495);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
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

        private System.Windows.Forms.PictureBox equationBox;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox codeTextbox;
        private System.Windows.Forms.Button colorBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}