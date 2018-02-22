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
			this.albumsLB = new System.Windows.Forms.ListBox();
			this.registerBtn = new MetroFramework.Controls.MetroButton();
			this.loginBtn = new MetroFramework.Controls.MetroButton();
			this.usernameTB = new MetroFramework.Controls.MetroTextBox();
			this.passwordTB = new MetroFramework.Controls.MetroTextBox();
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
			this.mysql.Location = new System.Drawing.Point(683, 36);
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
			this.searchBtn.Location = new System.Drawing.Point(52, 394);
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
			this.artistasLB.Location = new System.Drawing.Point(253, 249);
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
			this.searchSpoBtn.Location = new System.Drawing.Point(513, 14);
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
			this.artistaTB.Location = new System.Drawing.Point(62, 345);
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
			this.musicasLB.Location = new System.Drawing.Point(451, 249);
			this.musicasLB.Name = "musicasLB";
			this.musicasLB.Size = new System.Drawing.Size(152, 199);
			this.musicasLB.TabIndex = 10;
			this.musicasLB.SelectedIndexChanged += new System.EventHandler(this.musicasLB_SelectedIndexChanged);
			// 
			// albumsLB
			// 
			this.albumsLB.FormattingEnabled = true;
			this.albumsLB.Location = new System.Drawing.Point(625, 249);
			this.albumsLB.Name = "albumsLB";
			this.albumsLB.Size = new System.Drawing.Size(152, 199);
			this.albumsLB.TabIndex = 11;
			// 
			// registerBtn
			// 
			this.registerBtn.ForeColor = System.Drawing.SystemColors.ControlText;
			this.registerBtn.Location = new System.Drawing.Point(36, 113);
			this.registerBtn.Name = "registerBtn";
			this.registerBtn.Size = new System.Drawing.Size(147, 54);
			this.registerBtn.TabIndex = 13;
			this.registerBtn.Text = "Registar";
			this.registerBtn.UseSelectable = true;
			this.registerBtn.UseStyleColors = true;
			this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
			// 
			// loginBtn
			// 
			this.loginBtn.ForeColor = System.Drawing.SystemColors.ControlText;
			this.loginBtn.Location = new System.Drawing.Point(206, 113);
			this.loginBtn.Name = "loginBtn";
			this.loginBtn.Size = new System.Drawing.Size(147, 54);
			this.loginBtn.TabIndex = 14;
			this.loginBtn.Text = "Login";
			this.loginBtn.UseSelectable = true;
			this.loginBtn.UseStyleColors = true;
			this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
			// 
			// usernameTB
			// 
			// 
			// 
			// 
			this.usernameTB.CustomButton.Image = null;
			this.usernameTB.CustomButton.Location = new System.Drawing.Point(125, 1);
			this.usernameTB.CustomButton.Name = "";
			this.usernameTB.CustomButton.Size = new System.Drawing.Size(21, 21);
			this.usernameTB.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.usernameTB.CustomButton.TabIndex = 1;
			this.usernameTB.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.usernameTB.CustomButton.UseSelectable = true;
			this.usernameTB.CustomButton.Visible = false;
			this.usernameTB.Lines = new string[0];
			this.usernameTB.Location = new System.Drawing.Point(36, 187);
			this.usernameTB.MaxLength = 32767;
			this.usernameTB.Name = "usernameTB";
			this.usernameTB.PasswordChar = '\0';
			this.usernameTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.usernameTB.SelectedText = "";
			this.usernameTB.SelectionLength = 0;
			this.usernameTB.SelectionStart = 0;
			this.usernameTB.ShortcutsEnabled = true;
			this.usernameTB.Size = new System.Drawing.Size(147, 23);
			this.usernameTB.TabIndex = 15;
			this.usernameTB.UseSelectable = true;
			this.usernameTB.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.usernameTB.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// passwordTB
			// 
			// 
			// 
			// 
			this.passwordTB.CustomButton.Image = null;
			this.passwordTB.CustomButton.Location = new System.Drawing.Point(125, 1);
			this.passwordTB.CustomButton.Name = "";
			this.passwordTB.CustomButton.Size = new System.Drawing.Size(21, 21);
			this.passwordTB.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.passwordTB.CustomButton.TabIndex = 1;
			this.passwordTB.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.passwordTB.CustomButton.UseSelectable = true;
			this.passwordTB.CustomButton.Visible = false;
			this.passwordTB.Lines = new string[0];
			this.passwordTB.Location = new System.Drawing.Point(36, 216);
			this.passwordTB.MaxLength = 32767;
			this.passwordTB.Name = "passwordTB";
			this.passwordTB.PasswordChar = '\0';
			this.passwordTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.passwordTB.SelectedText = "";
			this.passwordTB.SelectionLength = 0;
			this.passwordTB.SelectionStart = 0;
			this.passwordTB.ShortcutsEnabled = true;
			this.passwordTB.Size = new System.Drawing.Size(147, 23);
			this.passwordTB.TabIndex = 16;
			this.passwordTB.UseSelectable = true;
			this.passwordTB.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.passwordTB.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.passwordTB);
			this.Controls.Add(this.usernameTB);
			this.Controls.Add(this.loginBtn);
			this.Controls.Add(this.registerBtn);
			this.Controls.Add(this.albumsLB);
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
        private System.Windows.Forms.ListBox albumsLB;
        private MetroFramework.Controls.MetroButton registerBtn;
        private MetroFramework.Controls.MetroButton loginBtn;
        private MetroFramework.Controls.MetroTextBox usernameTB;
        private MetroFramework.Controls.MetroTextBox passwordTB;
    }
}

