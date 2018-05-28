using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using SpotifyAPI.Web.Models;

namespace PAP
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<FullArtist> _artistsSpotify = new List<FullArtist>();
		private Spotify _spotify = new Spotify();
		private List<Artista> _artists = new List<Artista>();
		private List<Musica> _tracks = new List<Musica>();
		private List<Album> _albums = new List<Album>();
		private List<Playlist> playlists = new List<Playlist>();
		
		public MainWindow()
		{
			InitializeComponent();

			ContentSwitch.Content = new ArtistasOuvidos();

			//Get Music Disk
			DriveInfo[] myDrives = DriveInfo.GetDrives();

			try
			{
				foreach (DriveInfo drive in myDrives)
				{
					if (drive.VolumeLabel == "EXT")
					{
						Global.RootMusic = drive.Name;
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Source);
			}
			


			UsernameLabel.Content = LoginInfo.username;
			if (LoginInfo.username == "")
			{
				LoggedAsLabel.Content = "";
				LoginBtn.Content = "Login";
			}
			else
			{
				RegistarBtn.Visibility = Visibility.Hidden;

				if (LoginInfo.username == "admin")
				{
					AdminBtn.Visibility = Visibility.Visible;
				}
			}

			playlists = Global.sql.GetTodasPlaylists(LoginInfo.id);
			for(int i = 0; i < playlists.Count; i++)
			{
				playlistsLB.Items.Add(playlists[i].nome);
			}

		}

		/*
		private async void Init()
		{
			await DownloadTracks();
		}

		private async Task DownloadTracks()
		{
			List<Musica> musicas = Global.sql.GetTodasMusicas();

			for(int i = 0; i < musicas.Count; i++)
			{
				try
				{
					string m = musicas[i].Nome;
					Artista artista = Global.sql.ProcurarArtista(musicas[i].artista.id);

					//Youtube search
					VideoSearch items = new VideoSearch();
					var videos = items.SearchQuery(m + " " + artista.Nome, 1);
					string musica = videos[0].Url.Replace("http://www.youtube.com/watch?v=", "");

					var client = new YoutubeClient();
					var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(musica);

					var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
					var ext = streamInfo.Container.GetFileExtension();

					m = m.Replace("?", "");
					m = m.Replace("/", "");
					m = m.Replace("|", "");


					if (ext == "webm")
					{

					}
					else
					{
						await client.DownloadMediaStreamAsync(streamInfo, @"F:\PAPMusic\" + m + " - " + artista.Nome + "." + ext);
					}

					Console.WriteLine("Numero: " + i);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				
			}
		}
*/


		/*private void authButton_Click(object sender, EventArgs e)
		{
			Task.Run(() => _spotify.RunAuthentication());
		}

		private void mysql_Click(object sender, EventArgs e)
		{
			List<FullTrack> tracks = new List<FullTrack>();
			List<Musica> musicas = new List<Musica>();
			musicas = _sql.GetTodasMusicas();
			for (int i = 0; i < musicas.Count; i++)
			{
				tracks = _spotify.ProcurarMusicaSpotify(musicas[i].Nome, 1);
				if (tracks.Count > 0)
				{
					int cod = _sql.GetCodigoAlbum(tracks[0].Album.Name);
					if (cod == -1)
					{
						cod = _sql.GetTotalAlbums();
						cod++;
						_sql.InserirAlbum(cod, tracks[0].Album.Name, musicas[i].id);
						Console.WriteLine(tracks[0].Album.Name);
					}
					else
					{
						_sql.InserirAlbum(cod, tracks[0].Album.Name, musicas[i].id);
						Console.WriteLine(tracks[0].Album.Name);
					}
				}
			}
		}*/



		/*private void searchSpoBtn_Click(object sender, EventArgs e)
		{
			_artistsSpotify = _spotify.ProcurarArtistasSpotify(artistaTB.Text, 5);
			artistasLB.Items.Clear();
			for (int i = 0; i < _artistsSpotify.Count; i++)
			{
				artistasLB.Items.Add(_artistsSpotify[i].Name);
				artistaImgPB.ImageLocation = _artistsSpotify[i].Images[0].Url;
			}
			_spotifyArt = true;
		}*/



		/*private void registerBtn_Click(object sender, EventArgs e)
		{
			_sql.RegistarUtilizador(usernameTB.Text, passwordTB.Text);
		}*/



		private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			MediaPlayer.Volume = VolumeSlider.Value;
		}

		private void StopButton_Click(object sender, RoutedEventArgs e)
		{
			if (MediaPlayer.CanPause)
				MediaPlayer.Pause();
		}

		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			MediaPlayer.Play();
		}
		

		private void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
			if(LoginInfo.username == "")
			{
				LoginWindow login = new LoginWindow();
				login.Show();
				MediaPlayer.Stop();
				this.Close();
			}
			else
			{
				LoginInfo.username = "";
				LoginInfo.id = -1;
				LoggedAsLabel.Content = "";
				LoginBtn.Content = "Login";
				UsernameLabel.Content = "";
				RegistarBtn.Visibility = Visibility.Visible;

				MainWindow m = new MainWindow();
				m.Show();
				this.Close();
			}
		}

		private void FavoriteArtists_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "")
			{
				ContentSwitch.Content = new FavoriteArtistsUC();
			}
			else
			{
				LoginWindow w = new LoginWindow();
				w.Show();
				this.Close();
			}

		}

		private void FavoriteTracks_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "")
			{
				ContentSwitch.Content = new FavoriteSongUC();
			}
			else
			{
				LoginWindow w = new LoginWindow();
				w.Show();
				this.Close();
			}
		}

		private void FavoriteAlbums_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "")
			{
				ContentSwitch.Content = new FavoriteAlbumUC();
			}
			else
			{
				LoginWindow w = new LoginWindow();
				w.Show();
				this.Close();
			}
		}

		private void SearchBtn_Click(object sender, RoutedEventArgs e)
		{
			_tracks = _spotify.ProcurarMusicas(SearchTB.Text, 10);

			List<Musica> m = _spotify.ProcurarMusicasPorArtista(SearchTB.Text, 5);
			for (int i = 0; i < m.Count; i++)
			{
				_tracks.Add(m[i]);
			}

			_artists = _spotify.ProcurarArtistas(SearchTB.Text, 10);
			_albums = _spotify.ProcurarAlbums(SearchTB.Text, 10);

			ContentSwitch.Content = new SearchUC(_tracks, _artists, _albums);
		}

		private void RegistarBtn_Click(object sender, RoutedEventArgs e)
		{
			SignUpWindow window = new SignUpWindow();
			window.Show();
			this.Close();
		}

		private void AdminBtn_Click(object sender, RoutedEventArgs e)
		{
			AdminMain window = new AdminMain();
			window.Show();
			this.Close();
		}

		private void HomeBtn_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new ArtistasOuvidos();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(LoginInfo.username == "")
			{
				LoginWindow w = new LoginWindow();
				w.Show();
				this.Close();
			}
			else
			{
				string input = Microsoft.VisualBasic.Interaction.InputBox("Digite o nome da Playlist", "Popup", "Default", -1, -1);
				Global.sql.InserirPlaylist(input, -1);
				playlists.Clear();
				playlists = Global.sql.GetTodasPlaylists(LoginInfo.id);
				playlistsLB.Items.Add(input);
			}
			
		}

		private void DeletePlaylist_Click(object sender, RoutedEventArgs e)
		{
			int SI = playlistsLB.SelectedIndex;
			if(SI != -1)
			{
				int id = playlists[SI].id;
				Global.sql.DeletePlaylist(id);
				playlistsLB.Items.RemoveAt(SI);
				playlists.RemoveAt(SI);
			}
		}

		private void playlistsLB_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			int id = playlists[playlistsLB.SelectedIndex].id;
			ContentSwitch.Content = new PlaylistsUC(id);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MediaPlayer.Pause();
		}

		private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
		{
			if (Global.playlist_index == Global.playlist_max_index)
				Global.playlist_index = -1;
			string nome = Global.playlist[Global.playlist_index + 1].Nome;
			string nomeArtista = Global.playlist[Global.playlist_index + 1].artista.Nome;
			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";
			MediaPlayer.Source = new Uri(final);
			MediaPlayer.Play();
			Global.playlist_index++;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (Global.playlist_index == Global.playlist_max_index)
				Global.playlist_index = -1;
			string nome = Global.playlist[Global.playlist_index + 1].Nome;
			string nomeArtista = Global.playlist[Global.playlist_index + 1].artista.Nome;
			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";
			MediaPlayer.Source = new Uri(final);
			MediaPlayer.Play();
			Global.playlist_index++;
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			string nome = Global.playlist[Global.playlist_index - 1].Nome;
			string nomeArtista = Global.playlist[Global.playlist_index - 1].artista.Nome;
			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";
			MediaPlayer.Source = new Uri(final);
			MediaPlayer.Play();
			Global.playlist_index--;
		}
	}
}
