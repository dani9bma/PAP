using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PAP
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{

		public LoginWindow()
		{
			InitializeComponent();
		}

		private void loginBtn_Click(object sender, EventArgs e)
		{
			if(UsernameTb.Text == "admin")
			{
				if(PasswordTb.Password.ToString() == "adm")
				{
					LoginInfo.username = "admin";
					LoginInfo.id = -2;
					var mainWindow = new MainWindow();
					mainWindow.Show();
					this.Close();
				}
				else
				{
					MessageBox.Show("Password Incorreta");
				}
			}
			else if (UsernameTb.Text == "")
			{
				MessageBox.Show("Tem de preencher o username");
			}
			else if (PasswordTb.Password.ToString() == "")
			{
				MessageBox.Show("Tem de preencher o password");
			}
			else
			{
				if(Global.sql.LoginUtilizador(UsernameTb.Text, PasswordTb.Password.ToString()))
				{
					var mainWindow = new MainWindow();
					mainWindow.Show();
					this.Close();
				}
			}

		}
	}
}
