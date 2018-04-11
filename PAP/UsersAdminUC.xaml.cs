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
	/// Interaction logic for UsersAdminUC.xaml
	/// </summary>
	public partial class UsersAdminUC : UserControl
	{
		List<User> users = new List<User>();

		public UsersAdminUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			users = Global.sql.GetTodosUsers();

			for (int i = 0; i < users.Count; i++)
			{
				idLB.Items.Add(users[i].id);
				NomeUtilLB.Items.Add(users[i].username);
				PassUtilLB.Items.Add(users[i].password);
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
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(PassUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer2.VerticalOffset);
		}

		private void lbx2_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(NomeUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(PassUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void lbx3_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer2 = GetDescendantByType(NomeUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(PassUtilLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
			_listboxScrollViewer2.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (NomeUtilLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.Items[NomeUtilLB.SelectedIndex].ToString());
				int index = NomeUtilLB.SelectedIndex;
				Global.sql.DeleteUtilizador(cod);
				NomeUtilLB.Items.RemoveAt(index);
				idLB.Items.RemoveAt(index);
			}
			else if (idLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.SelectedItem.ToString());
				int index = idLB.SelectedIndex;
				Global.sql.DeleteUtilizador(cod);
				NomeUtilLB.Items.RemoveAt(index);
				idLB.Items.RemoveAt(index);
			}
		}

		private void Eliminar_Click(object sender, RoutedEventArgs e)
		{
			if (NomeUtilLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.Items[NomeUtilLB.SelectedIndex].ToString());
				int index = NomeUtilLB.SelectedIndex;
				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(AdminMain))
					{
						(window as AdminMain).ContentSwitch.Content = new AlterarUtilizador(cod);
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
						(window as AdminMain).ContentSwitch.Content = new AlterarUtilizador(cod);
					}
				}
			}
		}
	}
}
