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
	/// Interaction logic for AlterarMusica.xaml
	/// </summary>
	public partial class AlterarMusica : UserControl
	{
		private int _cod;
		private List<Artista> _artists = new List<Artista>();

		public AlterarMusica(int cod)
		{
			InitializeComponent();
			Init(cod);
		}

		private void Init(int cod)
		{
			_cod = cod;
			Musica musica = Global.sql.ProcurarMusica(_cod);
			idTxt.Text = musica.id.ToString();
			NameTxt.Text = musica.Nome;
			_artists = Global.sql.GetTodosArtistas();

			for(int i = 0; i < _artists.Count; i++)
			{
				ArtistaCB.Items.Add(_artists[i].Nome);
				if (_artists[i].id == musica.id)
					ArtistaCB.SelectedIndex = i;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int id_artista;
			id_artista = _artists[ArtistaCB.SelectedIndex].id;

			Global.sql.AlterarMusica(_cod, NameTxt.Text, id_artista);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(AdminMain))
				{
					(window as AdminMain).ContentSwitch.Content = new MusicasAdminUC();
				}
			}
		}
	}
}
