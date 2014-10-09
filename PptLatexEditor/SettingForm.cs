using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerPointLatex
{
    public partial class SettingForm : Form
    {
        public String latexPath { get; private set; }

        private static String appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static String configFile = appDataDir + @"\pptlatex\config.ini";

        public SettingForm()
        {
            InitializeComponent();

            //
            String initDir = @"C:\";
            if (File.Exists(configFile))
            {
                StreamReader reader = new StreamReader(configFile);
                String platexExe = reader.ReadLine();
                settingFileTextBox.Text = platexExe;
                initDir = Path.GetDirectoryName(platexExe);
                reader.Close();
            }

            // setting open file dialog
            openFileDialog1.InitialDirectory = initDir;
            openFileDialog1.Filter = "LaTeX executable (*.exe)|*.exe";
        }

        private void settingFileChooseButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                settingFileTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void setttingCancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void settingOkButton_Click(object sender, EventArgs e)
        {
            latexPath = settingFileTextBox.Text;
            this.Hide();

            // save config file
            StreamWriter writer = new StreamWriter(configFile);
            writer.WriteLine(latexPath);
            writer.Close();
        }
    }
}
