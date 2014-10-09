namespace PowerPointLatex
{
    partial class SettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.settingFileTextBox = new System.Windows.Forms.TextBox();
            this.setttingCancelButton = new System.Windows.Forms.Button();
            this.settingOkButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.settingFileChooseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "TeX Path";
            // 
            // settingFileTextBox
            // 
            this.settingFileTextBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.settingFileTextBox.Location = new System.Drawing.Point(16, 37);
            this.settingFileTextBox.Name = "settingFileTextBox";
            this.settingFileTextBox.Size = new System.Drawing.Size(316, 27);
            this.settingFileTextBox.TabIndex = 1;
            // 
            // setttingCancelButton
            // 
            this.setttingCancelButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.setttingCancelButton.Location = new System.Drawing.Point(226, 70);
            this.setttingCancelButton.Name = "setttingCancelButton";
            this.setttingCancelButton.Size = new System.Drawing.Size(75, 33);
            this.setttingCancelButton.TabIndex = 2;
            this.setttingCancelButton.Text = "Cancel";
            this.setttingCancelButton.UseVisualStyleBackColor = true;
            this.setttingCancelButton.Click += new System.EventHandler(this.setttingCancelButton_Click);
            // 
            // settingOkButton
            // 
            this.settingOkButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.settingOkButton.Location = new System.Drawing.Point(307, 70);
            this.settingOkButton.Name = "settingOkButton";
            this.settingOkButton.Size = new System.Drawing.Size(75, 33);
            this.settingOkButton.TabIndex = 3;
            this.settingOkButton.Text = "OK";
            this.settingOkButton.UseVisualStyleBackColor = true;
            this.settingOkButton.Click += new System.EventHandler(this.settingOkButton_Click);
            // 
            // settingFileChooseButton
            // 
            this.settingFileChooseButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.settingFileChooseButton.Location = new System.Drawing.Point(339, 37);
            this.settingFileChooseButton.Name = "settingFileChooseButton";
            this.settingFileChooseButton.Size = new System.Drawing.Size(43, 23);
            this.settingFileChooseButton.TabIndex = 4;
            this.settingFileChooseButton.Text = "...";
            this.settingFileChooseButton.UseVisualStyleBackColor = true;
            this.settingFileChooseButton.Click += new System.EventHandler(this.settingFileChooseButton_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 115);
            this.Controls.Add(this.settingFileChooseButton);
            this.Controls.Add(this.settingOkButton);
            this.Controls.Add(this.setttingCancelButton);
            this.Controls.Add(this.settingFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "SettingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox settingFileTextBox;
        private System.Windows.Forms.Button setttingCancelButton;
        private System.Windows.Forms.Button settingOkButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button settingFileChooseButton;
    }
}