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
	/// Interaction logic for AlterarArtista.xaml
	/// </summary>
	public partial class AlterarArtista : UserControl
	{
		private int _cod = -1;
		string newDest = "";

		public AlterarArtista(int cod)
		{
			InitializeComponent();
			Init(cod);
		}

		private void Init(int cod)
		{
			_cod = cod;
			Artista artista = Global.sql.ProcurarArtista(_cod);
			idTxt.Text = artista.id.ToString();
			NameTxt.Text = artista.Nome;
			ImageSourceConverter converter = new ImageSourceConverter();
			ArtImage.Source = (ImageSource)converter.ConvertFromString(artista.Img);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Global.sql.AlterarArtista(_cod, NameTxt.Text, newDest);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(AdminMain))
				{
					(window as AdminMain).ContentSwitch.Content = new ArtistsAdminUC();
				}
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

				if (File.Exists(newDest))
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
