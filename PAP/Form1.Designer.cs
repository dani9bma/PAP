namespace PAP
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.authButton = new MetroFramework.Controls.MetroButton();
            this.mysql = new MetroFramework.Controls.MetroButton();
            this.searchBtn = new MetroFramework.Controls.MetroButton();
            this.artistasLB = new System.Windows.Forms.ListBox();
            this.artistaImgPB = new System.Windows.Forms.PictureBox();
            this.searchSpoBtn = new MetroFramework.Controls.MetroButton();
            this.artistaTB = new MetroFramework.Controls.MetroTextBox();
            this.playMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.musicasLB = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.artistaImgPB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playMP)).BeginInit();
            this.SuspendLayout();
            // 
            // authButton
            // 
            this.authButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.authButton.Location = new System.Drawing.Point(2, 14);
            this.authButton.Name = "authButton";
            this.authButton.Size = new System.Drawing.Size(147, 54);
            this.authButton.TabIndex = 2;
            this.authButton.Text = "Authenticate";
            this.authButton.UseSelectable = true;
            this.authButton.UseStyleColors = true;
            this.authButton.Click += new System.EventHandler(this.authButton_Click);
            // 
            // mysql
            // 
            this.mysql.Location = new System.Drawing.Point(688, 48);
            this.mysql.Name = "mysql";
            this.mysql.Size = new System.Drawing.Size(107, 60);
            this.mysql.TabIndex = 3;
            this.mysql.Text = "MySql";
            this.mysql.UseSelectable = true;
            this.mysql.UseStyleColors = true;
            this.mysql.Click += new System.EventHandler(this.mysql_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchBtn.Location = new System.Drawing.Point(219, 249);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(147, 54);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "Procurar";
            this.searchBtn.UseSelectable = true;
            this.searchBtn.UseStyleColors = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // artistasLB
            // 
            this.artistasLB.FormattingEnabled = true;
            this.artistasLB.Location = new System.Drawing.Point(418, 249);
            this.artistasLB.Name = "artistasLB";
            this.artistasLB.Size = new System.Drawing.Size(152, 199);
            this.artistasLB.TabIndex = 5;
            this.artistasLB.SelectedIndexChanged += new System.EventHandler(this.artistasLB_SelectedIndexChanged);
            // 
            // artistaImgPB
            // 
            this.artistaImgPB.Location = new System.Drawing.Point(485, 95);
            this.artistaImgPB.Name = "artistaImgPB";
            this.artistaImgPB.Size = new System.Drawing.Size(159, 118);
            this.artistaImgPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.artistaImgPB.TabIndex = 6;
            this.artistaImgPB.TabStop = false;
            // 
            // searchSpoBtn
            // 
            this.searchSpoBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchSpoBtn.Location = new System.Drawing.Point(23, 249);
            this.searchSpoBtn.Name = "searchSpoBtn";
            this.searchSpoBtn.Size = new System.Drawing.Size(147, 54);
            this.searchSpoBtn.TabIndex = 7;
            this.searchSpoBtn.Text = "Procurar Spotify";
            this.searchSpoBtn.UseSelectable = true;
            this.searchSpoBtn.UseStyleColors = true;
            this.searchSpoBtn.Click += new System.EventHandler(this.searchSpoBtn_Click);
            // 
            // artistaTB
            // 
            // 
            // 
            // 
            this.artistaTB.CustomButton.Image = null;
            this.artistaTB.CustomButton.Location = new System.Drawing.Point(79, 1);
            this.artistaTB.CustomButton.Name = "";
            this.artistaTB.CustomButton.Size = new System.Drawing.Size(41, 41);
            this.artistaTB.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.artistaTB.CustomButton.TabIndex = 1;
            this.artistaTB.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.artistaTB.CustomButton.UseSelectable = true;
            this.artistaTB.CustomButton.Visible = false;
            this.artistaTB.Lines = new string[0];
            this.artistaTB.Location = new System.Drawing.Point(174, 134);
            this.artistaTB.MaxLength = 32767;
            this.artistaTB.Multiline = true;
            this.artistaTB.Name = "artistaTB";
            this.artistaTB.PasswordChar = '\0';
            this.artistaTB.PromptText = "Artista";
            this.artistaTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.artistaTB.SelectedText = "";
            this.artistaTB.SelectionLength = 0;
            this.artistaTB.SelectionStart = 0;
            this.artistaTB.ShortcutsEnabled = true;
            this.artistaTB.Size = new System.Drawing.Size(121, 43);
            this.artistaTB.TabIndex = 8;
            this.artistaTB.UseSelectable = true;
            this.artistaTB.UseStyleColors = true;
            this.artistaTB.WaterMark = "Artista";
            this.artistaTB.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.artistaTB.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // playMP
            // 
            this.playMP.Enabled = true;
            this.playMP.Location = new System.Drawing.Point(419, 523);
            this.playMP.Name = "playMP";
            this.playMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playMP.OcxState")));
            this.playMP.Size = new System.Drawing.Size(225, 45);
            this.playMP.TabIndex = 9;
            // 
            // musicasLB
            // 
            this.musicasLB.FormattingEnabled = true;
            this.musicasLB.Location = new System.Drawing.Point(610, 249);
            this.musicasLB.Name = "musicasLB";
            this.musicasLB.Size = new System.Drawing.Size(152, 199);
            this.musicasLB.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.musicasLB);
            this.Controls.Add(this.playMP);
            this.Controls.Add(this.artistaTB);
            this.Controls.Add(this.searchSpoBtn);
            this.Controls.Add(this.artistaImgPB);
            this.Controls.Add(this.artistasLB);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.mysql);
            this.Controls.Add(this.authButton);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.artistaImgPB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playMP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton authButton;
        private MetroFramework.Controls.MetroButton mysql;
        private MetroFramework.Controls.MetroButton searchBtn;
        private System.Windows.Forms.ListBox artistasLB;
        private System.Windows.Forms.PictureBox artistaImgPB;
        private MetroFramework.Controls.MetroButton searchSpoBtn;
        private MetroFramework.Controls.MetroTextBox artistaTB;
        private AxWMPLib.AxWindowsMediaPlayer playMP;
        private System.Windows.Forms.ListBox musicasLB;
    }
}

