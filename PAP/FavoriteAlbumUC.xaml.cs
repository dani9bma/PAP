﻿using System;
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
				Album album = Global.sql.ProcurarAlbum(codAlbums[i].id);
				Albums.Add(album);
				FavoriteAlbumsLB.Items.Add(album.Nome);
			}
		}

		private void FavoriteArtistsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
