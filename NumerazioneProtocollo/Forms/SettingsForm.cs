using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NumerazioneProtocollo.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SaveDirFiles(fbd.SelectedPath);
                    this.textBox_path_files.Text = fbd.SelectedPath;
                }
            }
        }

        private static void SaveDirFiles(string selectedPath)
        {
            Data.GlobalVariables.paths ??= new Model.Rif<Model.Path.SettingsVar>();
            Data.GlobalVariables.paths.Obj ??= new Model.Path.SettingsVar();
            Data.GlobalVariables.paths.Obj.DirPath = selectedPath;
            Utils.Files.SaveFile(Data.GlobalVariables.paths, Data.Constants.PathOfSettings);
        }

        private void textBox_path_files_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(this.textBox_path_files.Text))
            {
                SaveDirFiles(this.textBox_path_files.Text); 
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Data.GlobalVariables.paths ??= new Model.Rif<Model.Path.SettingsVar>();
            Data.GlobalVariables.paths.Obj ??= new Model.Path.SettingsVar();
            this.textBox_path_files.Text = Data.GlobalVariables.paths.Obj.DirPath;

        }
    }
}
