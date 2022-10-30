namespace BotwSaveManager.UI
{
    partial class Help
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
            this.pbExample = new System.Windows.Forms.PictureBox();
            this.lblHelp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbExample)).BeginInit();
            this.SuspendLayout();
            // 
            // pbExample
            // 
            this.pbExample.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbExample.Image = global::BotwSaveManager.UI.Properties.Resources.example;
            this.pbExample.Location = new System.Drawing.Point(0, 40);
            this.pbExample.Name = "pbExample";
            this.pbExample.Size = new System.Drawing.Size(585, 217);
            this.pbExample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbExample.TabIndex = 0;
            this.pbExample.TabStop = false;
            // 
            // lblHelp
            // 
            this.lblHelp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHelp.Location = new System.Drawing.Point(0, 0);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(585, 37);
            this.lblHelp.TabIndex = 1;
            this.lblHelp.Text = "This is what the folder you are selecting should contain.\r\nDo NOT select one of t" +
    "hese folders; select the folder that contains these items.";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 257);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.pbExample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Help";
            this.ShowIcon = false;
            this.Text = "Help";
            ((System.ComponentModel.ISupportInitialize)(this.pbExample)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbExample;
        private System.Windows.Forms.Label lblHelp;
    }
}