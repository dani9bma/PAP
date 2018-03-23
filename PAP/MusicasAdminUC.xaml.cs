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
	/// Interaction logic for MusicasAdminUC.xaml
	/// </summary>
	public partial class MusicasAdminUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();

		public MusicasAdminUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			musicas = Global.sql.GetTodasMusicas();

			for (int i = 0; i < musicas.Count; i++)
			{
				idLB.Items.Add(musicas[i].id);
				NomeMusicaLB.Items.Add(musicas[i].Nome);
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
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeMusicaLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void lbx3_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeMusicaLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
		}
	}
}
