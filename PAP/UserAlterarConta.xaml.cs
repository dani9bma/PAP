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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAP
{
	/// <summary>
	/// Interaction logic for UserAlterarConta.xaml
	/// </summary>
	public partial class UserAlterarConta : UserControl
	{
		int id = LoginInfo.id;

		public UserAlterarConta()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			User user = Global.sql.GetUtilizador(id);
			NomeTb.Text = user.nome;
			UsernameTb.Text = user.username;
			Passwordbox.Password = user.password;
			ConfPasswordbox.Password = user.password;
		}

		private void loginBtn_Click(object sender, RoutedEventArgs e)
		{
			if (Passwordbox.Password.ToString() == ConfPasswordbox.Password.ToString())
				Global.sql.AlterarUtilizador(id, UsernameTb.Text, Passwordbox.Password.ToString(), NomeTb.Text);
			else
				MessageBox.Show("As passwords têm de corresponder");
		}
	}
}
