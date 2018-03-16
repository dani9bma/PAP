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
	/// Interaction logic for FavoriteArtistsUC.xaml
	/// </summary>
	public partial class FavoriteArtistsUC : UserControl
	{
		List<Artista> Artistas = new List<Artista>();

		public FavoriteArtistsUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			int cod = LoginInfo.id;

			List<Artista> codArtistas = Global.sql.GetArtistasFavoritos(cod);

			for (int i = 0; i < codArtistas.Count; i++)
			{
				Artista artista = Global.sql.ProcurarArtista(codArtistas[i].id);
				Artistas.Add(artista);

				FavoriteArtistsLB.Items.Add(Artistas[i].Nome);
			}
		}

		private void FavoriteArtistsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = FavoriteArtistsLB.SelectedIndex;
			int cod = Global.sql.GetCodigoArtista(Artistas[pos].Nome);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(cod);
				}
			}
		}
	}
}
