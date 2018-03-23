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
	/// Interaction logic for FavoriteMusicAdminUC.xaml
	/// </summary>
	public partial class FavoriteMusicAdminUC : UserControl
	{
		List<MusicasFavoritas> musicas = new List<MusicasFavoritas>();

		public FavoriteMusicAdminUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			musicas = Global.sql.GetMusicasFavoritasAdmin();

			for (int i = 0; i < musicas.Count; i++)
			{
				idLB.Items.Add(musicas[i].id_user);
				NomeUtilLB.Items.Add(musicas[i].user);
				NomeMusicLB.Items.Add(musicas[i].musica);
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
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(NomeUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeMusicLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
		}

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(NomeUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeMusicLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void lbx3_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(NomeUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeMusicLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
		}
	}
}
