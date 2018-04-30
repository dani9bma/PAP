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
	/// Interaction logic for MainUC.xaml
	/// </summary>
	public partial class MainUC : UserControl
	{
		private Spotify _spotify = new Spotify();
		private List<Artista> _artists = new List<Artista>();
		private List<Musica> _tracks = new List<Musica>();
		private List<Album> _albums = new List<Album>();
		private List<Playlist> playlists = new List<Playlist>();
		private int MusicaPos = -1;

		public MainUC()
		{
			InitializeComponent();

			if(LoginInfo.username == "")
			{
				PlaylistsCB.Visibility = Visibility.Hidden;
				AddPlaylist.Visibility = Visibility.Hidden;
				DeletePlaylist.Visibility = Visibility.Hidden;
			}
			else
			{
				playlists = Global.sql.GetTodasPlaylists(LoginInfo.id);
				for (int i = 0; i < playlists.Count; i++)
				{
					PlaylistsCB.Items.Add(playlists[i].nome);
				}
				PlaylistsCB.Items.Add("Criar Nova");

				PlaylistsCB.Visibility = Visibility.Visible;
				AddPlaylist.Visibility = Visibility.Visible;
				DeletePlaylist.Visibility = Visibility.Visible;
			}
			
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

		private async void MusicasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			MusicaPos = MusicasLb.SelectedIndex;

			if(MusicaPos != -1)
			{
				Console.WriteLine(_tracks[MusicaPos].Nome);
				Console.WriteLine(_tracks[MusicaPos].artista.Nome);

				MediaElement player = new MediaElement();

				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(MainWindow))
					{
						player = (window as MainWindow).MediaPlayer;
					}
				}

				player.Source = null;

				await Global.sql.DownloadFiles(_tracks[MusicaPos].Nome);
				player.Source = new Uri(Path.GetTempPath() + "music.mp4");
				player.Play();
				Console.WriteLine(player.Source);
			}
		}

		private void ArtistasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = ArtistasLb.SelectedIndex;
			var converter = new ImageSourceConverter();

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
			int pos = AlbumsLb.SelectedIndex;

			int cod = Global.sql.GetCodigoAlbum(_albums[pos].Nome);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new AlbumUC(cod);
				}
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (PlaylistsCB.SelectedItem.ToString() == "Criar Nova")
			{
				string input = Microsoft.VisualBasic.Interaction.InputBox("Digite o nome da Playlist", "Popup", "Default", -1, -1);
				Global.sql.InserirPlaylist(input, -1);

				MainWindow w = new MainWindow();
				w.Show();

				var myWindow = Window.GetWindow(this);
				myWindow.Close();
			}
		}

		private void AddPlaylist_Click(object sender, RoutedEventArgs e)
		{
			if(PlaylistsCB.SelectedItem.ToString() != "Criar Nova")
			{
				Global.sql.InserirPlaylist(PlaylistsCB.SelectedItem.ToString(), _tracks[MusicaPos].id);
			}
		}

		private void DeletePlaylist_Click(object sender, RoutedEventArgs e)
		{
			var index = PlaylistsCB.SelectedIndex;
			var item = PlaylistsCB.SelectedItem;
			int id = Global.sql.GetPlaylistByNome(item.ToString());
			Global.sql.DeletePlaylist(id);

			MainWindow w = new MainWindow();
			w.Show();

			var myWindow = Window.GetWindow(this);
			myWindow.Close();
		}

	}
}
