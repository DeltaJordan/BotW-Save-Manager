namespace BotWSaveManager.UI
{
    partial class MainForm
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
            this.menuStripFile = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConvert = new System.Windows.Forms.TabPage();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbSaveLocation = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblConsoleName = new System.Windows.Forms.Label();
            this.pbConsole = new System.Windows.Forms.PictureBox();
            this.lblFileWarning = new System.Windows.Forms.Label();
            this.menuStripFile.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabConvert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripFile
            // 
            this.menuStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStripFile.Location = new System.Drawing.Point(0, 0);
            this.menuStripFile.Name = "menuStripFile";
            this.menuStripFile.Size = new System.Drawing.Size(1034, 24);
            this.menuStripFile.TabIndex = 0;
            this.menuStripFile.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSaveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openSaveToolStripMenuItem
            // 
            this.openSaveToolStripMenuItem.Name = "openSaveToolStripMenuItem";
            this.openSaveToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.openSaveToolStripMenuItem.Text = "Open Save...";
            this.openSaveToolStripMenuItem.Click += new System.EventHandler(this.OpenSaveToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConvert);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1034, 498);
            this.tabControl1.TabIndex = 1;
            // 
            // tabConvert
            // 
            this.tabConvert.Controls.Add(this.btnSaveToFile);
            this.tabConvert.Controls.Add(this.btnBrowse);
            this.tabConvert.Controls.Add(this.tbSaveLocation);
            this.tabConvert.Controls.Add(this.btnConvert);
            this.tabConvert.Controls.Add(this.lblConsoleName);
            this.tabConvert.Controls.Add(this.pbConsole);
            this.tabConvert.Location = new System.Drawing.Point(4, 22);
            this.tabConvert.Name = "tabConvert";
            this.tabConvert.Padding = new System.Windows.Forms.Padding(3);
            this.tabConvert.Size = new System.Drawing.Size(1026, 472);
            this.tabConvert.TabIndex = 0;
            this.tabConvert.Text = "Conversion";
            this.tabConvert.UseVisualStyleBackColor = true;
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(697, 418);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToFile.TabIndex = 5;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Visible = false;
            this.btnSaveToFile.Click += new System.EventHandler(this.BtnSaveToFile_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(616, 418);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Visible = false;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // tbSaveLocation
            // 
            this.tbSaveLocation.Location = new System.Drawing.Point(22, 420);
            this.tbSaveLocation.Name = "tbSaveLocation";
            this.tbSaveLocation.Size = new System.Drawing.Size(588, 20);
            this.tbSaveLocation.TabIndex = 4;
            this.tbSaveLocation.Visible = false;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(634, 32);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(113, 57);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Convert!";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Visible = false;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // lblConsoleName
            // 
            this.lblConsoleName.Location = new System.Drawing.Point(114, 6);
            this.lblConsoleName.Name = "lblConsoleName";
            this.lblConsoleName.Size = new System.Drawing.Size(260, 100);
            this.lblConsoleName.TabIndex = 2;
            this.lblConsoleName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbConsole
            // 
            this.pbConsole.Location = new System.Drawing.Point(8, 6);
            this.pbConsole.Name = "pbConsole";
            this.pbConsole.Size = new System.Drawing.Size(100, 100);
            this.pbConsole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbConsole.TabIndex = 1;
            this.pbConsole.TabStop = false;
            // 
            // lblFileWarning
            // 
            this.lblFileWarning.AutoSize = true;
            this.lblFileWarning.BackColor = System.Drawing.Color.White;
            this.lblFileWarning.Location = new System.Drawing.Point(328, 222);
            this.lblFileWarning.Name = "lblFileWarning";
            this.lblFileWarning.Size = new System.Drawing.Size(238, 13);
            this.lblFileWarning.TabIndex = 0;
            this.lblFileWarning.Text = "Please select a save file from the context menu...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 522);
            this.Controls.Add(this.lblFileWarning);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStripFile);
            this.MainMenuStrip = this.menuStripFile;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Breath of the Wild Save Manager";
            this.menuStripFile.ResumeLayout(false);
            this.menuStripFile.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabConvert.ResumeLayout(false);
            this.tabConvert.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConsole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSaveToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabConvert;
        private System.Windows.Forms.Label lblFileWarning;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lblConsoleName;
        private System.Windows.Forms.PictureBox pbConsole;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbSaveLocation;
    }
}

