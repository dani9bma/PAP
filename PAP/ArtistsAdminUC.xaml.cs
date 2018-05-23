using System;
using System.Collections.Generic;
using System.IO;
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
	/// Interaction logic for ArtistsAdminUC.xaml
	/// </summary>
	public partial class ArtistsAdminUC : UserControl
	{
		List<Artista> artistas = new List<Artista>();
		string newDest;

		public ArtistsAdminUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			artistas = Global.sql.GetTodosArtistas();

			for (int i = 0; i < artistas.Count; i++)
			{
				idLB.Items.Add(artistas[i].id);
				NomeArtLB.Items.Add(artistas[i].Nome);
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
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeArtLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer3.ScrollToVerticalOffset(_listboxScrollViewer1.VerticalOffset);
		}

		private void lbx3_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			ScrollViewer _listboxScrollViewer1 = GetDescendantByType(idLB, typeof(ScrollViewer)) as ScrollViewer;
			ScrollViewer _listboxScrollViewer3 = GetDescendantByType(NomeArtLB, typeof(ScrollViewer)) as ScrollViewer;
			_listboxScrollViewer1.ScrollToVerticalOffset(_listboxScrollViewer3.VerticalOffset);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (NomeArtLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.Items[NomeArtLB.SelectedIndex].ToString());
				int index = NomeArtLB.SelectedIndex;
				Global.sql.DeleteArtists(cod);
				NomeArtLB.Items.RemoveAt(index);
				idLB.Items.RemoveAt(index);
			}
			else if (idLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.SelectedItem.ToString());
				int index = idLB.SelectedIndex;
				Global.sql.DeleteArtists(cod);
				NomeArtLB.Items.RemoveAt(index);
				idLB.Items.RemoveAt(index);
			}
		}

		private void Alterar_Click(object sender, RoutedEventArgs e)
		{
			if (NomeArtLB.SelectedIndex >= 0)
			{
				int cod = int.Parse(idLB.Items[NomeArtLB.SelectedIndex].ToString());
				int index = NomeArtLB.SelectedIndex;
				foreach (Window window in Application.Current.Windows)
				{
					if (window.GetType() == typeof(AdminMain))
					{
						(window as AdminMain).ContentSwitch.Content = new AlterarArtista(cod);
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
						(window as AdminMain).ContentSwitch.Content = new AlterarArtista(cod);
					}
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (NameTxt.Text.Length > 0 && MusicNameTxt.Text.Length > 0 && newDest.Length > 0)
			{
				Global.sql.InserirArtistas(NameTxt.Text, newDest);
				int cod = Global.sql.GetCodigoArtista(NameTxt.Text);
				Global.sql.InserirMusicas(MusicNameTxt.Text, cod);
			}
			else
			{
				MessageBox.Show("Tem de preencher todos os campos");
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			dlg.DefaultExt = ".png";
			dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

			
			Nullable<bool> result = dlg.ShowDialog();

			if (result == true)
			{
				string path = dlg.FileName;
				string filename = dlg.SafeFileName;
				ImgTxt.Text = path;
				string current = Directory.GetCurrentDirectory();
				current = current.Replace(@"\", @"/");
				string replace = "";
				if (Directory.Exists(current + "/Images"))
				{
					newDest = current + @"/Images/" + filename;
					replace = current + @"/" + filename;
				}
				else
				{
					Directory.CreateDirectory(current + "/Images");
					newDest = current + @"/Images/" + filename;
					replace = current + @"/" + filename;
				}
				
				if(File.Exists(newDest))
				{
					File.Replace(path, newDest, replace);
					File.Delete(replace);
					File.Copy(newDest, path);
				}
				else
				{
					File.Copy(path, newDest);
				}
			}
		}
	}
}
