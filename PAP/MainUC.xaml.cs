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
	/// Interaction logic for MainUC.xaml
	/// </summary>
	public partial class MainUC : UserControl
	{
		private Spotify _spotify = new Spotify();
		private List<Artista> _artists = new List<Artista>();
		private List<Musica> _tracks = new List<Musica>();
		private List<Album> _albums = new List<Album>();

		public MainUC()
		{
			InitializeComponent();
		}

		private void searchBtn_Click(object sender, EventArgs e)
		{

			_tracks = _spotify.ProcurarMusicas(SearchTb.Text, 10);
			MusicasLb.Items.Clear();

			List<Musica> m = _spotify.ProcurarMusicasPorArtista(SearchTb.Text, 5);
			for (int i = 0; i < m.Count; i++)
			{
				_tracks.Add(m[i]);
			}

			for (int i = 0; i < _tracks.Count; i++)
			{
				MusicasLb.Items.Add(_tracks[i].Nome);
				Console.WriteLine(_tracks[i].Nome);
				Console.WriteLine(_tracks[i].artista.Nome);
			}

			_artists = _spotify.ProcurarArtistas(SearchTb.Text, 10);
			ArtistasLb.Items.Clear();
			for (int i = 0; i < _artists.Count; i++)
			{
				ArtistasLb.Items.Add(_artists[i].Nome);
				Console.WriteLine(_artists[i].Nome);
			}

			_albums = _spotify.ProcurarAlbums(SearchTb.Text, 10);
			AlbumsLb.Items.Clear();
			for (int i = 0; i < _albums.Count; i++)
			{
				AlbumsLb.Items.Add(_albums[i].Nome);
				Console.WriteLine(_albums[i].Nome);
			}
		}

		private void MusicasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = MusicasLb.SelectedIndex;

			Console.WriteLine(_tracks[pos].Nome);
			Console.WriteLine(_tracks[pos].artista.Nome);

			MediaElement player = new MediaElement();

			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					player = (window as MainWindow).MediaPlayer;
				}
			}

			player.Source = null;

			Global.sql.DownloadFiles(_tracks[pos].Nome);
			player.Source = new Uri(@"C:\Users\Daniel Assunção\Documents\PAP\PAP\bin\Debug\music.mp4");
			player.Play();
			Console.WriteLine(player.Source);


		}

		private void ArtistasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = ArtistasLb.SelectedIndex;
			var converter = new ImageSourceConverter();
			ArtistaImage.Source = (ImageSource)converter.ConvertFromString(_artists[pos].Img);

			int cod = Global.sql.GetCodigoArtista(_artists[pos].Nome);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(cod);
				}
			}
		}

		private void albumsLB_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void AddFavoriteTrack_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "" && LoginInfo.id != -1)
			{
				AddToFavorite(1);
			}
			else
			{
				LoginWindow login = new LoginWindow();
				login.Show();
			}
		}

		private void AddFavoriteArtist_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "" && LoginInfo.id != -1)
			{
				AddToFavorite(2);
			}
			else
			{
				LoginWindow login = new LoginWindow();
				login.Show();
			}
		}

		private void AddFavoriteAlbum_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "" && LoginInfo.id != -1)
			{
				AddToFavorite(3);
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
		private void AddToFavorite(int type)
		{
			int pos;
			switch (type)
			{
				case 1:
					pos = MusicasLb.SelectedIndex;
					Global.sql.InserirMusicasFavoritas(_tracks[pos].id, LoginInfo.id);
					break;
				case 2:
					pos = ArtistasLb.SelectedIndex;
					Global.sql.InserirArtistasFavoritos(_artists[pos].id, LoginInfo.id);
					break;
				case 3:
					pos = AlbumsLb.SelectedIndex;
					Global.sql.InserirAlbumsFavoritos(_albums[pos].id, LoginInfo.id);
					break;
			}
		}
	}
}
