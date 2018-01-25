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
            this.authButton = new MetroFramework.Controls.MetroButton();
            this.mysql = new MetroFramework.Controls.MetroButton();
            this.searchBtn = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // authButton
            // 
            this.authButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.authButton.Location = new System.Drawing.Point(137, 63);
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
            this.mysql.Location = new System.Drawing.Point(355, 57);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.mysql);
            this.Controls.Add(this.authButton);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton authButton;
        private MetroFramework.Controls.MetroButton mysql;
        private MetroFramework.Controls.MetroButton searchBtn;
    }
}

