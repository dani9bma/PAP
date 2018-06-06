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
	/// Interaction logic for AlterarUtilizador.xaml
	/// </summary>
	public partial class AlterarUtilizador : UserControl
	{
		private int _cod;

		public AlterarUtilizador(int cod)
		{
			InitializeComponent();
			Init(cod);
		}

		private void Init(int cod)
		{
			_cod = cod;
			User user = Global.sql.GetUtilizador(_cod);
			idTxt.Text = user.id.ToString();
			NameTxt.Text = user.username;
			PassTxt.Text = user.password;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Global.sql.AlterarUtilizador(_cod, NameTxt.Text, PassTxt.Text, NomeTxt.Text);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(AdminMain))
				{
					(window as AdminMain).ContentSwitch.Content = new UsersAdminUC();
				}
			}
		}
	}
}
