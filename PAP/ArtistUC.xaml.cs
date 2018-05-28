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
using System.IO;

namespace PAP
{
	/// <summary>
	/// Interaction logic for ArtistUC.xaml
	/// </summary>
	public partial class ArtistUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();
		List<Playlist> _playlists = new List<Playlist>();
		int codArt;

		public ArtistUC(int cod)
		{
			InitializeComponent();
			InitPage(cod);
			codArt = cod;
		}

		private void InitPage(int id_artista)
		{
			Artista artista = Global.sql.ProcurarArtista(id_artista);
			var converter = new ImageSourceConverter();
			ArtistPicture.Source = (ImageSource)converter.ConvertFromString(artista.Img);
			ArtistName.Content = artista.Nome;

			musicas = Global.sql.GetTodasMusicasArtista(id_artista);
			for (int i = 0; i < musicas.Count; i++)
			{
				ArtistTracksLB.Items.Add(musicas[i].Nome);
			}

			_playlists = Global.sql.GetPlaylistsUser(LoginInfo.id);
			for (int i = 0; i < _playlists.Count; i++)
			{
				PlaylistsCB.Items.Add(_playlists[i].nome);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "" && LoginInfo.id != -1)
			{
				AddToFavorite();
			}
			else
			{
				LoginWindow login = new LoginWindow();
				login.Show();
			}
		}


		/*
		 * type:
		 *	1: Track
		 *	2: Artist
		 *	3: Album
		 */
		private void AddToFavorite()
		{
			Global.sql.InserirArtistasFavoritos(codArt, LoginInfo.id);
		}

		private void ArtistTracksLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			string nome = ArtistTracksLB.SelectedItem.ToString();
			string nomeArtista = ArtistName.Content.ToString();
			if (LoginInfo.username != "")
				Global.sql.InserirArtistaOuvido(codArt, nomeArtista);
			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";

			Global.playlist_index = ArtistTracksLB.SelectedIndex;
			Global.playlist_max_index = ArtistTracksLB.Items.Count - 1;


			Global.playlist.Clear();
			for (int i = 0; i < ArtistTracksLB.Items.Count; i++)
			{
				Musica musica = new Musica();
				musica.Nome = ArtistTracksLB.Items[i].ToString();
				musica.artista.Nome = nomeArtista;
				Global.playlist.Add(musica);
			}

			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).MediaPlayer.Source = new Uri(final);
					(window as MainWindow).MediaPlayer.Play();
				}
			}

		}

		private void AddFavorite_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "" && LoginInfo.id != -1)
			{
				AddToFavorite_Music();
			}
			else
			{
				LoginWindow login = new LoginWindow();
				login.Show();
				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(MainWindow))
					{
						(window as MainWindow).Close();
					}
				}
			}
		}

		private void AddToFavorite_Music()
		{
			int pos = ArtistTracksLB.SelectedIndex;
			int cod = musicas[pos].id;
			Global.sql.InserirMusicasFavoritas(cod, LoginInfo.id);
		}

		private void AddPlaylist_Click(object sender, RoutedEventArgs e)
		{
			Global.sql.InserirPlaylist(PlaylistsCB.SelectedItem.ToString(), musicas[ArtistTracksLB.SelectedIndex].id);
		}
	}
}
