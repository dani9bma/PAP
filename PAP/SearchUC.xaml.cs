using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PAP
{
	/// <summary>
	/// Interaction logic for SearchUC.xaml
	/// </summary>
	public partial class SearchUC : UserControl
	{
		private List<Artista> _artists = new List<Artista>();
		private List<Musica> _tracks = new List<Musica>();
		private List<Album> _albums = new List<Album>();
		private List<Playlist> _playlists = new List<Playlist>();

		public SearchUC(List<Musica> tracks, List<Artista> artists, List<Album> albums)
		{
			InitializeComponent();
			ArtArtLB.Items.Clear();
			ArtArtLB2.Items.Clear();
			ArtArtLB3.Items.Clear();
			MusMusLB.Items.Clear();
			MusArtLB.Items.Clear();
			AlbAlbLB.Items.Clear();
			AlbArtLB.Items.Clear();

			_playlists = Global.sql.GetPlaylistsUser(LoginInfo.id);
			for (int i = 0; i < _playlists.Count; i++)
			{
				PlaylistsCB.Items.Add(_playlists[i].nome);
			}

			_artists = artists;
			_tracks = tracks;
			_albums = albums;

			if (_artists.Count >= 12)
			{
				for (int i = 0; i < 4; i++)
					ArtArtLB.Items.Add(_artists[i].Nome);
				for (int i = 4; i < 8; i++)
					ArtArtLB2.Items.Add(_artists[i].Nome);
				for (int i = 8; i < 12; i++)
					ArtArtLB3.Items.Add(_artists[i].Nome);
			}
			else if (_artists.Count >= 8)
			{
				for (int i = 0; i < 4; i++)
					ArtArtLB.Items.Add(_artists[i].Nome);
				for (int i = 4; i < 8; i++)
					ArtArtLB2.Items.Add(_artists[i].Nome);
			}
			else if (_artists.Count >= 4)
			{
				for (int i = 0; i < 4; i++)
					ArtArtLB.Items.Add(_artists[i].Nome);
			}
			else
			{
				for (int i = 0; i < (_artists.Count); i++)
					ArtArtLB.Items.Add(_artists[i].Nome);
			}

			for (int i = 0; i < (_tracks.Count); i++)
			{
				MusMusLB.Items.Add(_tracks[i].Nome);
				MusArtLB.Items.Add(_tracks[i].artista.Nome);
			}

			for (int i = 0; i < (_albums.Count); i++)
			{
				AlbAlbLB.Items.Add(_albums[i].Nome);
				AlbArtLB.Items.Add(_albums[i].Musicas[0].artista.Nome);
			}
		}

		public Visual GetDescendantByType(Visual element, Type type)
		{
			if (element == null) return null;
			if (element.GetType() == type) return element;
			Visual foundElement = null;
			if (element is FrameworkElement)
			{
				(element as FrameworkElement).ApplyTemplate();
			}
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
			{
				Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
				foundElement = GetDescendantByType(visual, type);
				if (foundElement != null)
					break;
			}
			return foundElement;
		}

		private void lbx1_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(AlbAlbLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(AlbArtLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
		}

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(AlbAlbLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(AlbArtLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void lbx3_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(MusMusLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer4 = GetDescendantByType(MusArtLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer4.VerticalOffset);
		}

		private void lbx4_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(MusMusLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer4 = GetDescendantByType(MusArtLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer4.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
		}

		private void ArtArtLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(_artists[ArtArtLB.SelectedIndex].id);
				}
			}
		}

		private void ArtArtLB2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(_artists[4 + (ArtArtLB2.SelectedIndex)].id);
				}
			}
		}

		private void ArtArtLB3_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(_artists[8 + (ArtArtLB3.SelectedIndex)].id);
				}
			}
		}

		private void MusArtLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(_tracks[MusArtLB.SelectedIndex].artista.id);
				}
			}
		}

		private void AlbAlbLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new AlbumUC(_albums[AlbAlbLB.SelectedIndex].id);
				}
			}
		}

		private void AlbArtLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(_albums[AlbArtLB.SelectedIndex].Musicas[0].artista.id);
				}
			}
		}

		//Musicas
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (PlaylistsCB.SelectedItem != null)
			{
				Global.sql.InserirPlaylist(PlaylistsCB.SelectedItem.ToString(), _tracks[MusMusLB.SelectedIndex].id);
			}
			else
			{
				MessageBox.Show("Tem de selecionar uma playlist");
				var brush = new SolidColorBrush();
				var color = new Color();
				color.R = 255;
				color.G = 0;
				color.B = 0;
				color.A = 255;
				brush.Color = color;
				PlaylistsCB.BorderBrush = brush;
			}
		}

		private void AddFavorites_Click(object sender, RoutedEventArgs e)
		{
			if (LoginInfo.username != "" && LoginInfo.id != -1)
			{
				AddToFavorite();
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

		/*
		 * type:
		 *	1: Track
		 *	2: Artist
		 *	3: Album
		 */
		private void AddToFavorite()
		{
			int pos = MusMusLB.SelectedIndex;
			int cod = _tracks[pos].id;
			Global.sql.InserirMusicasFavoritas(cod, LoginInfo.id);
		}

		private void MusMusLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			string nome = MusMusLB.SelectedItem.ToString();
			string nomeArtista = MusArtLB.Items[MusMusLB.SelectedIndex].ToString();
			int codArt = _tracks[MusMusLB.SelectedIndex].artista.id;
			if(LoginInfo.username != "")
				Global.sql.InserirArtistaOuvido(codArt, nomeArtista);

			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";

			Global.playlist_index = MusMusLB.SelectedIndex;
			Global.playlist_max_index = MusMusLB.Items.Count - 1;

			Global.playlist.Clear();
			for (int i = 0; i < MusMusLB.Items.Count; i++)
			{
				Musica musica = new Musica();
				musica.Nome = MusMusLB.Items[i].ToString();
				musica.artista.Nome = MusArtLB.Items[i].ToString();
				Global.playlist.Add(musica);
			}

			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).MediaPlayer.Source = new Uri(final);
					(window as MainWindow).mediaPlayerTimer = new DispatcherTimer();
					(window as MainWindow).mediaPlayerTimer.IsEnabled = true;
					(window as MainWindow).mediaPlayerTimer.Interval = TimeSpan.FromMilliseconds(1000);
					(window as MainWindow).mediaPlayerTimer.Tick += (window as MainWindow).MediaPlayerTimer_Tick;
					(window as MainWindow).MediaPlayer.Play();
				}
			}
		}

	}
}