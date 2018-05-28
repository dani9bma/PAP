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
	/// Interaction logic for AlbumUC.xaml
	/// </summary>
	public partial class AlbumUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();
		int codMusica;

		public AlbumUC(int cod)
		{
			InitializeComponent();
			InitPage(cod);
			codMusica = cod;
		}

		private void InitPage(int id_album)
		{
			Album album = Global.sql.ProcurarAlbumMusicas(id_album);

			AlbumName.Content = album.Nome;

			for(int i = 0; i < album.Musicas.Count; i++)
			{
				Musica musica = new Musica();
				musica = Global.sql.ProcurarMusica(album.Musicas[i].id);
				musicas.Add(musica);
			}

			ArtName.Content = musicas[0].artista.Nome;
	
			for (int i = 0; i < musicas.Count; i++)
			{
				AlbumTracksLB.Items.Add(musicas[i].Nome);
			}

			List<Playlist> playlists = Global.sql.GetPlaylistsUser(LoginInfo.id);
			for(int i = 0; i < playlists.Count; i++)
			{
				PlaylistsCB.Items.Add(playlists[i].nome);
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
			Global.sql.InserirAlbumsFavoritos(codMusica, LoginInfo.id);
		}

		private void AdicionarPlaylist_Click(object sender, RoutedEventArgs e)
		{
			for(int i = 0; i < musicas.Count; i++)
			{
				Global.sql.InserirPlaylist(PlaylistsCB.SelectedItem.ToString(), musicas[i].id);
			}
		}

		private void AlbumTracksLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			string nome = AlbumTracksLB.SelectedItem.ToString();
			string nomeArtista = musicas[AlbumTracksLB.SelectedIndex].artista.Nome;
			int codArt = musicas[0].artista.id;
			if (LoginInfo.username != "")
				Global.sql.InserirArtistaOuvido(codArt, nomeArtista);
			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";

			Global.playlist_index = AlbumTracksLB.SelectedIndex;
			Global.playlist_max_index = AlbumTracksLB.Items.Count - 1;


			Global.playlist.Clear();  
			for (int i = 0; i < AlbumTracksLB.Items.Count; i++)
			{
				Musica musica = new Musica();
				musica.Nome = AlbumTracksLB.Items[i].ToString();
				musica.artista.Nome = musicas[i].artista.Nome;
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
			int pos = AlbumTracksLB.SelectedIndex;
			int cod = musicas[pos].id;
			Global.sql.InserirMusicasFavoritas(cod, LoginInfo.id);
		}

		private void AddPlaylist_Click(object sender, RoutedEventArgs e)
		{
			Global.sql.InserirPlaylist(PlaylistsCB.SelectedItem.ToString(), musicas[AlbumTracksLB.SelectedIndex].id);
		}
	}
}
