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
	/// Interaction logic for AlterarArtista.xaml
	/// </summary>
	public partial class AlterarArtista : UserControl
	{
		private int _cod = -1;

		public AlterarArtista(int cod)
		{
			InitializeComponent();
			Init(cod);
		}

		private void Init(int cod)
		{
			_cod = cod;
			Artista artista = Global.sql.ProcurarArtista(_cod);
			idTxt.Text = artista.id.ToString();
			NameTxt.Text = artista.Nome;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Global.sql.AlterarArtista(_cod, NameTxt.Text);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(AdminMain))
				{
					(window as AdminMain).ContentSwitch.Content = new ArtistsAdminUC();
				}
			}
		}
	}
}
