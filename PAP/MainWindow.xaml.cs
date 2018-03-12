using System;
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
		private Spotify _spotify = new Spotify();
		private List<FullArtist> _artistsSpotify = new List<FullArtist>();
		private List<Artista> _artists = new List<Artista>();
		private List<Musica> _tracks = new List<Musica>();
		private List<Album> _albums = new List<Album>();
		private bool _spotifyArt = false;
		private List<Musica> _musicas = new List<Musica>();

		public MainWindow()
		{
			InitializeComponent();
		}


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

		private void searchBtn_Click(object sender, EventArgs e)
		{
			/*_artists = _spotify.ProcurarArtistas(artistaTB.Text, 10);
            artistasLB.Items.Clear();
            for (int i = 0; i < _artists.Count; i++)
            {
                artistasLB.Items.Add(_artists[i].Nome);
                artistaImgPB.ImageLocation = _artists[i].Img;
            }

            _spotifyArt = false;*/

			// TODO: Tirar spotifyArt
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

			_spotifyArt = false;

			_artists = _spotify.ProcurarArtistas(SearchTb.Text, 10);
			ArtistasLb.Items.Clear();
			for (int i = 0; i < _artists.Count; i++)
			{
				ArtistasLb.Items.Add(_artists[i].Nome);
				Console.WriteLine(_artists[i].Nome);
			}
			_spotifyArt = false;

			_albums = _spotify.ProcurarAlbums(SearchTb.Text, 10);
			AlbumsLb.Items.Clear();
			for (int i = 0; i < _albums.Count; i++)
			{
				AlbumsLb.Items.Add(_albums[i].Nome);
				Console.WriteLine(_albums[i].Nome);
			}

			_spotifyArt = false;
		}

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

		private void MusicasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = MusicasLb.SelectedIndex;

			/*if (_spotifyArt)
                artistaImgPB.ImageLocation = _artistsSpotify[pos].Images[0].Url;
            else
                artistaImgPB.ImageLocation = _artists[pos].Img;*/
			Console.WriteLine(_tracks[pos].Nome);
			Console.WriteLine(_tracks[pos].artista.Nome);

			Uri source = new Uri(Global._sql.DownloadFiles(_tracks[pos].Nome, _tracks[pos].artista.Nome));

			MediaPlayer.Source = null;
			MediaPlayer.Source = source;
			MediaPlayer.Play();
			Console.WriteLine(MediaPlayer.Source);

			Global._sql.InserirMusicasFavoritas(_tracks[pos].id, LoginInfo.id);
		}

		/*private void registerBtn_Click(object sender, EventArgs e)
		{
			_sql.RegistarUtilizador(usernameTB.Text, passwordTB.Text);
		}*/

		private void ArtistasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			var converter = new ImageSourceConverter();
			int pos = ArtistasLb.SelectedIndex;
			ArtistaImage.Source = (ImageSource)converter.ConvertFromString(_artists[pos].Img);

			Global._sql.InserirArtistasFavoritos(_artists[pos].id, LoginInfo.id);
		}

		private void albumsLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = AlbumsLb.SelectedIndex;

			Global._sql.InserirAlbumsFavoritos(_albums[pos].id, LoginInfo.id);
		}

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
	}
}
