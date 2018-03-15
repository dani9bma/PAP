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
	/// Interaction logic for FavoriteArtistsWindow.xaml
	/// </summary>
	public partial class FavoriteArtistsWindow : Window
	{
		List<Artista> Artistas = new List<Artista>();

		public FavoriteArtistsWindow()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			int cod = LoginInfo.id;

			List<Artista> codArtistas = Global._sql.GetArtistasFavoritos(cod);

			for(int i = 0; i < codArtistas.Count; i++)
			{
				Artista artista = Global._sql.GetArtistaCodigo(codArtistas[i].id);
				Artistas.Add(artista);

				FavoriteArtistsLB.Items.Add(Artistas[i].Nome);
			}
		}

		private void FavoriteArtistsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = FavoriteArtistsLB.SelectedIndex;
			int cod = Global._sql.GetCodigoArtista(Artistas[pos].Nome);
			ArtistWindow atWindow = new ArtistWindow(cod);
			atWindow.Show();
			this.Close();
		}
	}
}
