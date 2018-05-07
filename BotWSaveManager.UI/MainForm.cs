using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotWSaveManager.Conversion;
using BotWSaveManager.UI.Properties;

namespace BotWSaveManager.UI
{
    public partial class MainForm : Form
    {
        public Save SelectedSave;
        public byte[] ConvertedSaveBytes;

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void OpenSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog
            {
                Filter = "Save files (*.sav)|*.sav|All files (*.*)|*.*"
            };

            if (dia.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(dia.FileName))
                {
                    MessageBox.Show("Please select a valid file before continuing.");
                }

                this.SelectedSave = new Save(dia.FileName);

                this.ConvertedSaveBytes = File.ReadAllBytes(this.SelectedSave.FileLocation);

                this.lblFileWarning.Hide();

                this.pbConsole.Image = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? Resources.nintendo_switch_logo : Resources.wii_u_logo;
                this.lblConsoleName.Text = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? "BotW - Switch" : "BotW - Wii U";

                this.btnConvert.Show();
                this.tbSaveLocation.Show();
                this.btnBrowse.Show();
                this.btnSaveToFile.Show();
            }
            else
            {
                MessageBox.Show("Please select a file before continuing.");
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                this.ConvertedSaveBytes = this.SelectedSave.ConvertSave();
            }
            catch (Exception exception)
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, $"error-{DateTime.Now.ToString(CultureInfo.CurrentCulture)}.log"), exception.ToString());

                MessageBox.Show("Error converting save file! Create an issue at https://github.com/JordanZeotni/BotW-Save-Manager with the error log and describe your game progress to the best of your ability.", exception.Message);

                this.Enabled = true;
                return;
            }

            MessageBox.Show("Save converted successfully! Either continue with the save editor or just save the file and copy it to your console.");

            this.Enabled = true;

            this.pbConsole.Image = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? Resources.nintendo_switch_logo : Resources.wii_u_logo;
            this.lblConsoleName.Text = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? "BotW - Switch" : "BotW - Wii U";
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dia = new SaveFileDialog
            {
                Filter = "Save file (*.sav)|*.sav"
            };

            if (dia.ShowDialog() == DialogResult.OK)
            {
                this.tbSaveLocation.Text = dia.FileName;
            }
        }

        private void BtnSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllBytes(this.tbSaveLocation.Text, this.ConvertedSaveBytes);
            }
            catch (Exception exception)
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, $"error-{DateTime.Now.ToString(CultureInfo.CurrentCulture)}.log"), exception.ToString());

                MessageBox.Show("Error writing save file to disk! Create an issue at https://github.com/JordanZeotni/BotW-Save-Manager with the error log **IF** you think its **NOT** a permission error.", exception.Message);
            }

            MessageBox.Show("File saved successfully!");
        }
    }
}
