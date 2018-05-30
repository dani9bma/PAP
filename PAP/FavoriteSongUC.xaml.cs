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
				ArtistsLB.Items.Add(musica.artista.Nome);
			}
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
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(FavoriteSongsLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(ArtistsLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
		}

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(FavoriteSongsLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(ArtistsLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void FavoriteSongsLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			string nome = FavoriteSongsLB.SelectedItem.ToString();
			string nomeArtista = Musicas[FavoriteSongsLB.SelectedIndex].artista.Nome;
			int codArt = Musicas[FavoriteSongsLB.SelectedIndex].artista.id;
			if (LoginInfo.username != "")
				Global.sql.InserirArtistaOuvido(codArt, nomeArtista);

			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";

			Global.playlist_index = FavoriteSongsLB.SelectedIndex;
			Global.playlist_max_index = FavoriteSongsLB.Items.Count - 1;

			Global.playlist.Clear();
			for (int i = 0; i < FavoriteSongsLB.Items.Count; i++)
			{
				Musica musica = new Musica();
				musica.Nome = FavoriteSongsLB.Items[i].ToString();
				musica.artista.Nome = FavoriteSongsLB.Items[i].ToString();
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
