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
	/// Interaction logic for PlaylistsUC.xaml
	/// </summary>
	public partial class PlaylistsUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();
		int id_playlist = 0;

		public PlaylistsUC(int id)
		{
			InitializeComponent();
			id_playlist = id;
			InitWindow(id);
		}

		private void InitWindow(int id)
		{
			musicas = Global.sql.GetPlaylistsMusicas(id);
			
			for(int i = 0; i < musicas.Count; i++)
			{
				TracksLB.Items.Add(musicas[i].Nome);
				ArtistsLB.Items.Add(musicas[i].artista.Nome);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int SI = TracksLB.SelectedIndex;
			int id_musica = musicas[SI].id;
			Global.sql.DeleteMusicaPlaylist(id_playlist, id_musica);
		}

		private void TracksLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			string nome = TracksLB.SelectedItem.ToString();
			string nomeArtista = musicas[TracksLB.SelectedIndex].artista.Nome;
			int codArt = musicas[TracksLB.SelectedIndex].artista.id;
			if (LoginInfo.username != "")
				Global.sql.InserirArtistaOuvido(codArt, nomeArtista);

			string final = Global.RootMusic + @"PAPMusic\" + nome + " - " + nomeArtista + @".mp4";

			Global.playlist_index = TracksLB.SelectedIndex;
			Global.playlist_max_index = TracksLB.Items.Count - 1;

			Global.playlist.Clear();
			for (int i = 0; i < TracksLB.Items.Count; i++)
			{
				Musica musica = new Musica();
				musica.Nome = TracksLB.Items[i].ToString();
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
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(TracksLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(ArtistsLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
		}

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(TracksLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(ArtistsLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}
	}
}
