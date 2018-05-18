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
	/// Interaction logic for SignUpWindow.xaml
	/// </summary>
	public partial class SignUpWindow : Window
	{
		public SignUpWindow()
		{
			InitializeComponent();
		}

		private void loginBtn_Click(object sender, RoutedEventArgs e)
		{
			if(Global.sql.RegistarUtilizador(UsernameTb.Text, PasswordTb.Password.ToString(), NomeTb.Text))
			{
				MessageBox.Show("Registado com sucesso");
				this.Close();
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MainWindow w = new MainWindow();
			w.Show();
		}
	}
}
