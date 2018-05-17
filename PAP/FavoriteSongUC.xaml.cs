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
	/// Interaction logic for FavoriteSongUC.xaml
	/// </summary>
	public partial class FavoriteSongUC : UserControl
	{
		List<Musica> Musicas = new List<Musica>();

		public FavoriteSongUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			int cod = LoginInfo.id;

			List<Musica> codMusicas = Global.sql.GetMusicasFavoritas(cod);

			for (int i = 0; i < codMusicas.Count; i++)
			{
				Musica musica = Global.sql.ProcurarMusica(codMusicas[i].id);
				Musicas.Add(musica);
				FavoriteSongsLB.Items.Add(musica.Nome);
			}
		}

		private void FavoriteArtistsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = FavoriteSongsLB.SelectedIndex;
			// Musicas[pos].Nome
			// Tocar Musica
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int pos = FavoriteSongsLB.SelectedIndex;
			int cod = Musicas[pos].id;
			Global.sql.RemoverMusicasFavoritas(cod, LoginInfo.id);
			FavoriteSongsLB.Items.RemoveAt(pos);
			Musicas.Clear();
			InitWindow();
		}
	}
}
