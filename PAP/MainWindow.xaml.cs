﻿using System;
using System.Collections.Generic;
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
		private bool dark = true;

		public MainWindow()
		{
			InitializeComponent();
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
				MessageBox.Show("Precisa de fazer login para ver os seus artistas favoritos");
			}

		}

		private void FavoriteTracks_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "")
			{
				ContentSwitch.Content = new MusicUC();
			}
			else
			{
				MessageBox.Show("Precisa de fazer login para ver as suas musicas favoritas");
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
				MessageBox.Show("Precisa de fazer login para ver as suas musicas favoritas");
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

			for (int i = 0; i < _tracks.Count; i++)
			{
				SearchLB.Items.Add(_tracks[i].Nome);
			}

			_artists = _spotify.ProcurarArtistas(SearchTB.Text, 10);
			for (int i = 0; i < _artists.Count; i++)
			{
				SearchLB.Items.Add(_artists[i].Nome);
			}

			_albums = _spotify.ProcurarAlbums(SearchTB.Text, 10);
			for (int i = 0; i < _albums.Count; i++)
			{
				SearchLB.Items.Add(_albums[i].Nome);
			}

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

		//Quando se clica na playlist
		private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ContentSwitch.Content = new PlaylistsUC(playlistsLB.SelectedItem.ToString());
		}

		private void HomeBtn_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new MainUC();
		}

		private void MudarTema(object sender, RoutedEventArgs e)
		{
			if(dark)
			{
				Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.Absolute) };
				dark = false;
			}
			else
			{
				Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.Absolute) };
				dark = true;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string input = Microsoft.VisualBasic.Interaction.InputBox("Digite o nome da Playlist", "Popup", "Default", -1, -1);
			Global.sql.InserirPlaylist(input, -1);

			playlistsLB.Items.Add(input);
		}
	}
}
