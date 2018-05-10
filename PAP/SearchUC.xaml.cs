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
	/// Interaction logic for SearchUC.xaml
	/// </summary>
	public partial class SearchUC : UserControl
	{
		public SearchUC(List<Musica> _tracks, List<Artista> _artists, List<Album> _albums)
		{
			InitializeComponent();
			ArtArtLB.Items.Clear();
			ArtArtLB2.Items.Clear();
			ArtArtLB3.Items.Clear();
			MusMusLB.Items.Clear();
			MusArtLB.Items.Clear();
			AlbAlbLB.Items.Clear();
			AlbArtLB.Items.Clear();

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
				for (int i = 0; i < (_artists.Count - 1); i++)
					ArtArtLB.Items.Add(_artists[i].Nome);
			}

			for (int i = 0; i < (_tracks.Count - 1); i++)
			{
				MusMusLB.Items.Add(_tracks[i].Nome);
				MusArtLB.Items.Add(_tracks[i].artista.Nome);
			}

			for (int i = 0; i < (_albums.Count - 1); i++)
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
	}
}
