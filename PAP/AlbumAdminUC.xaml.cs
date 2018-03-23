using System;
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
		List<Album> artistas = new List<Album>();

		public AlbumAdminUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			artistas = Global.sql.GetTodosAlbums();

			for (int i = 0; i < artistas.Count; i++)
			{
				idLB.Items.Add(artistas[i].id);
				NomeAlbumLB.Items.Add(artistas[i].Nome);
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
	}
}
