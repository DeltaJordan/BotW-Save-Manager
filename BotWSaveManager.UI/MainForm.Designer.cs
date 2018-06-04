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
            this.btnSaveToFiles = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbSaveLocation = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblConsoleName = new System.Windows.Forms.Label();
            this.pbConsole = new System.Windows.Forms.PictureBox();
            this.lblFileWarning = new System.Windows.Forms.Label();
            this.menuStripFile.SuspendLayout();
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
            this.menuStripFile.Size = new System.Drawing.Size(462, 24);
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
            // btnSaveToFiles
            // 
            this.btnSaveToFiles.Location = new System.Drawing.Point(345, 276);
            this.btnSaveToFiles.Name = "btnSaveToFiles";
            this.btnSaveToFiles.Size = new System.Drawing.Size(75, 23);
            this.btnSaveToFiles.TabIndex = 5;
            this.btnSaveToFiles.Text = "Save to File";
            this.btnSaveToFiles.UseVisualStyleBackColor = true;
            this.btnSaveToFiles.Visible = false;
            this.btnSaveToFiles.Click += new System.EventHandler(this.BtnSaveToFile_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(345, 247);
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
            this.tbSaveLocation.Location = new System.Drawing.Point(3, 249);
            this.tbSaveLocation.Name = "tbSaveLocation";
            this.tbSaveLocation.Size = new System.Drawing.Size(336, 20);
            this.tbSaveLocation.TabIndex = 4;
            this.tbSaveLocation.Visible = false;
            // 
            // btnConvert
            // 
            this.btnConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvert.Location = new System.Drawing.Point(287, 45);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(163, 57);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Convert!";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Visible = false;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // lblConsoleName
            // 
            this.lblConsoleName.Location = new System.Drawing.Point(109, 27);
            this.lblConsoleName.Name = "lblConsoleName";
            this.lblConsoleName.Size = new System.Drawing.Size(103, 172);
            this.lblConsoleName.TabIndex = 2;
            // 
            // pbConsole
            // 
            this.pbConsole.Location = new System.Drawing.Point(3, 27);
            this.pbConsole.Name = "pbConsole";
            this.pbConsole.Size = new System.Drawing.Size(100, 100);
            this.pbConsole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbConsole.TabIndex = 1;
            this.pbConsole.TabStop = false;
            // 
            // lblFileWarning
            // 
            this.lblFileWarning.AutoSize = true;
            this.lblFileWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblFileWarning.Location = new System.Drawing.Point(109, 130);
            this.lblFileWarning.Name = "lblFileWarning";
            this.lblFileWarning.Size = new System.Drawing.Size(238, 13);
            this.lblFileWarning.TabIndex = 0;
            this.lblFileWarning.Text = "Please select a save file from the context menu...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 307);
            this.Controls.Add(this.lblFileWarning);
            this.Controls.Add(this.pbConsole);
            this.Controls.Add(this.lblConsoleName);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.tbSaveLocation);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnSaveToFiles);
            this.Controls.Add(this.menuStripFile);
            this.MainMenuStrip = this.menuStripFile;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Breath of the Wild Save Manager";
            this.menuStripFile.ResumeLayout(false);
            this.menuStripFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbConsole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSaveToolStripMenuItem;
        private System.Windows.Forms.Label lblFileWarning;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lblConsoleName;
        private System.Windows.Forms.PictureBox pbConsole;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSaveToFiles;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbSaveLocation;
    }
}

