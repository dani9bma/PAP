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
	/// Interaction logic for FavoriteAlbumUC.xaml
	/// </summary>
	public partial class FavoriteAlbumUC : UserControl
	{
		List<Album> Albums = new List<Album>();

		public FavoriteAlbumUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			int cod = LoginInfo.id;

			List<Album> codAlbums = Global.sql.GetAlbumsFavoritos(cod);

			for (int i = 0; i < codAlbums.Count; i++)
			{
				Album album = Global.sql.ProcurarAlbumMusicas(codAlbums[i].id);
				Albums.Add(album);
				FavoriteAlbumsLB.Items.Add(album.Nome);
				ArtistsLB.Items.Add(album.Musicas[0].artista.Nome);
			}
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int pos = FavoriteAlbumsLB.SelectedIndex;
			int cod = Global.sql.GetCodigoAlbum(Albums[pos].Nome);
			Global.sql.RemoverAlbumsFavoritos(cod, LoginInfo.id);
			FavoriteAlbumsLB.Items.RemoveAt(pos);
			Albums.Clear();
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
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(FavoriteAlbumsLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(ArtistsLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
		}

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(FavoriteAlbumsLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(ArtistsLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void FavoriteAlbumsLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			int pos = FavoriteAlbumsLB.SelectedIndex;
			int cod = Global.sql.GetCodigoAlbum(Albums[pos].Nome);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new AlbumUC(cod);
				}
			}
		}
	}
}
