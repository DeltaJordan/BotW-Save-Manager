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
using BotWSaveManager.Conversion.IO;
using BotWSaveManager.UI.Properties;

namespace BotWSaveManager.UI
{
    public partial class MainForm : Form
    {
        public Save SelectedSave;
        public Dictionary<string, byte[]> SaveFilesDictionary;

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void OpenSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog
            {
                Filter = "Option save file (option.sav)|option.sav|All files (*.*)|*.*"
            };

            if (dia.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(dia.FileName))
                {
                    MessageBox.Show("Please select a valid file before continuing.");
                }

                try
                {
                    this.SelectedSave = new Save(dia.FileName);
                }
                catch (UnsupportedSaveException exception)
                {
                    MessageBox.Show(exception.Message);
                    this.btnConvert.Hide();
                    this.tbSaveLocation.Hide();
                    this.btnBrowse.Hide();
                    this.btnSaveToFile.Hide();
                    this.pbConsole.Image = null;
                    this.lblConsoleName.Text = "";
                    return;
                }

                this.SaveFilesDictionary = new Dictionary<string, byte[]>();

                foreach (string file in Directory.GetFiles(this.SelectedSave.SaveFolder, "*.sav", SearchOption.AllDirectories))
                {
                    this.SaveFilesDictionary.Add(file, File.ReadAllBytes(file));
                }

                this.lblFileWarning.Hide();

                this.pbConsole.Image = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? Resources.nintendo_switch_logo : Resources.wii_u_logo;
                this.lblConsoleName.Text = (this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? $"BotW {this.SelectedSave.GameVersion} - Switch" : $"BotW {this.SelectedSave.GameVersion} - Wii U");

                this.btnConvert.Show();
                this.tbSaveLocation.Show();
                this.btnBrowse.Show();
                this.btnSaveToFile.Show();
            }
            else
            {
                MessageBox.Show("Please select option.sav in your save folder before continuing.");
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                this.SaveFilesDictionary = this.SelectedSave.ConvertSave();
            }
            catch (Exception exception)
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, $"error-{DateTime.Now.ToString(CultureInfo.CurrentCulture)}.log"), exception.ToString());

                MessageBox.Show("Error converting save file! Create an issue at https://github.com/JordanZeotni/BotW-Save-Manager with the error log and describe your game progress to the best of your ability.", exception.Message);

                this.Enabled = true;
                return;
            }

            MessageBox.Show("Save converted successfully! Either continue with the save editor (not supported currently) or just save the file and copy it to your Nintendo console.");

            this.Enabled = true;

            this.pbConsole.Image = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? Resources.nintendo_switch_logo : Resources.wii_u_logo;
            this.lblConsoleName.Text = this.SelectedSave.SaveConsoleType == Save.SaveType.Switch ? $"BotW {this.SelectedSave.GameVersion} - Switch" : $"BotW {this.SelectedSave.GameVersion} - Wii U";
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();

            if (dia.ShowDialog() == DialogResult.OK)
            {
                this.tbSaveLocation.Text = dia.SelectedPath;
            }
        }

        private void BtnSaveToFile_Click(object sender, EventArgs eventArgs)
        {
            try
            {
                CopyDir.Copy(this.SelectedSave.SaveFolder, this.tbSaveLocation.Text);

                foreach (KeyValuePair<string, byte[]> convertSaveByte in this.SaveFilesDictionary)
                {
                    string saveTo = Directory.GetFiles(this.tbSaveLocation.Text, "*.sav", SearchOption.AllDirectories)
                        .First(e => Path.GetFileName(e) == "option.sav" ||
                                    Path.GetFileName(e) == Path.GetFileName(convertSaveByte.Key) &&
                                    Directory.GetParent(e).Name == Directory.GetParent(convertSaveByte.Key).Name);

                    File.WriteAllBytes(saveTo, convertSaveByte.Value);
                }
            }
            catch (Exception exception)
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, $"error-{DateTime.Now.ToString(CultureInfo.CurrentCulture)}.log"), exception.ToString());

                MessageBox.Show("Error writing save files to disk! Create an issue at https://github.com/JordanZeotni/BotW-Save-Manager with the error log **IF** you think its **NOT** a permission error.", exception.Message);
            }

            MessageBox.Show($"Files written successfully! Please note that you are required to use {this.SelectedSave.GameVersion} of BotW on the target system with (probably) the same DLC for this save file to work.");
        }
    }
}
