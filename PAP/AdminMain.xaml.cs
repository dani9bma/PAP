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
using System.Windows.Shapes;

namespace PAP
{
	/// <summary>
	/// Interaction logic for AdminMain.xaml
	/// </summary>
	public partial class AdminMain : Window
	{
		public AdminMain()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MainWindow window = new MainWindow();
			window.Show();
			this.Close();
		}

		private void FavoriteArtists_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new FavoriteArtistsAdminUC();
		}

		private void FavoriteTracks_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new FavoriteMusicAdminUC();
		}

		private void FavoriteAlbums_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new FavoriteAlbumsAdminUC();
		}

		private void Users_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new UsersAdminUC();
		}

		private void Albums_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new AlbumAdminUC();
		}

		private void Artists_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new ArtistsAdminUC();
		}

		private void Tracks_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new MusicasAdminUC();
		}

		private void Playlists_Click(object sender, RoutedEventArgs e)
		{
			ContentSwitch.Content = new PlaylistsAdminUC();
		}
	}
}
