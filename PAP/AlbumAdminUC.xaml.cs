﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PAP
{
	/// <summary>
	/// Interaction logic for AlbumAdminUC.xaml
	/// </summary>
	public partial class AlbumAdminUC : UserControl
	{
		List<Album> albums = new List<Album>();
		List<Artista> artistas = new List<Artista>();

		public AlbumAdminUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			albums = Global.sql.GetTodosAlbums();

			for (int i = 0; i < albums.Count; i++)
			{
				idLB.Items.Add(albums[i].id);
				NomeAlbumLB.Items.Add(albums[i].Nome);
			}

			artistas = Global.sql.GetTodosArtistas();

			for (int i = 0; i < artistas.Count; i++)
			{
				ArtistsCB.Items.Add(artistas[i].Nome);
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

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeAlbumLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void lbx3_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeAlbumLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (NomeAlbumLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.Items[NomeAlbumLB.SelectedIndex].ToString());
				int index = NomeAlbumLB.SelectedIndex;
				Global.sql.DeleteAlbum(cod);
				NomeAlbumLB.Items.RemoveAt(index);
				idLB.Items.RemoveAt(index);
			}
			else if (idLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.SelectedItem.ToString());
				int index = idLB.SelectedIndex;
				Global.sql.DeleteAlbum(cod);
				NomeAlbumLB.Items.RemoveAt(index);
				idLB.Items.RemoveAt(index);
			}
		}

		private void Alterar_Click(object sender, RoutedEventArgs e)
		{
			if (NomeAlbumLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.Items[NomeAlbumLB.SelectedIndex].ToString());
				int index = NomeAlbumLB.SelectedIndex;
				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(AdminMain))
					{
						(window as AdminMain).ContentSwitch.Content = new AlterarAlbum(cod);
					}
				}
			}
			else if (idLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.SelectedItem.ToString());
				int index = idLB.SelectedIndex;
				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(AdminMain))
					{
						(window as AdminMain).ContentSwitch.Content = new AlterarAlbum(cod);
					}
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (ArtistsCB.SelectedIndex > 0 && NameTxt.Text.Length > 0)
			{
				int pos = ArtistsCB.SelectedIndex;
				int cod = Global.sql.GetTotalAlbums();
				Global.sql.InserirAlbum(cod, NameTxt.Text, -1, artistas[pos].id);
				idLB.Items.Add(cod);
				NomeAlbumLB.Items.Add(NameTxt.Text);
			}

		}
	}
}
