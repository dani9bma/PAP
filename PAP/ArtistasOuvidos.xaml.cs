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
	/// Interaction logic for ArtistasOuvidos.xaml
	/// </summary>
	public partial class ArtistasOuvidos : UserControl
	{
		List<Artista> artistas = new List<Artista>();

		public ArtistasOuvidos()
		{
			InitializeComponent();
			artistas = Global.sql.GetArtistasOuvidos(LoginInfo.id);

			int i = artistas.Count;
			switch(i)
			{
				case 1:
					var converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					break;

				case 2:
					converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					converter = new ImageSourceConverter();
					Image_2.Source = (ImageSource)converter.ConvertFromString(artistas[1].Img);
					Label_2.Content = artistas[1].Nome;
					break;

				case 3:
					converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					converter = new ImageSourceConverter();
					Image_2.Source = (ImageSource)converter.ConvertFromString(artistas[1].Img);
					Label_2.Content = artistas[1].Nome;
					converter = new ImageSourceConverter();
					Image_3.Source = (ImageSource)converter.ConvertFromString(artistas[2].Img);
					Label_3.Content = artistas[2].Nome;
					break;

				case 4:
					converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					converter = new ImageSourceConverter();
					Image_2.Source = (ImageSource)converter.ConvertFromString(artistas[1].Img);
					Label_2.Content = artistas[1].Nome;
					converter = new ImageSourceConverter();
					Image_3.Source = (ImageSource)converter.ConvertFromString(artistas[2].Img);
					Label_3.Content = artistas[2].Nome;
					converter = new ImageSourceConverter();
					Image_4.Source = (ImageSource)converter.ConvertFromString(artistas[3].Img);
					Label_4.Content = artistas[3].Nome;
					break;

				case 5:
					converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					converter = new ImageSourceConverter();
					Image_2.Source = (ImageSource)converter.ConvertFromString(artistas[1].Img);
					Label_2.Content = artistas[1].Nome;
					converter = new ImageSourceConverter();
					Image_3.Source = (ImageSource)converter.ConvertFromString(artistas[2].Img);
					Label_3.Content = artistas[2].Nome;
					converter = new ImageSourceConverter();
					Image_4.Source = (ImageSource)converter.ConvertFromString(artistas[3].Img);
					Label_4.Content = artistas[3].Nome;
					converter = new ImageSourceConverter();
					Image_5.Source = (ImageSource)converter.ConvertFromString(artistas[4].Img);
					Label_5.Content = artistas[4].Nome;
					break;

				case 6:
					converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					converter = new ImageSourceConverter();
					Image_2.Source = (ImageSource)converter.ConvertFromString(artistas[1].Img);
					Label_2.Content = artistas[1].Nome;
					converter = new ImageSourceConverter();
					Image_3.Source = (ImageSource)converter.ConvertFromString(artistas[2].Img);
					Label_3.Content = artistas[2].Nome;
					converter = new ImageSourceConverter();
					Image_4.Source = (ImageSource)converter.ConvertFromString(artistas[3].Img);
					Label_4.Content = artistas[3].Nome;
					converter = new ImageSourceConverter();
					Image_5.Source = (ImageSource)converter.ConvertFromString(artistas[4].Img);
					Label_5.Content = artistas[4].Nome;
					converter = new ImageSourceConverter();
					Image_6.Source = (ImageSource)converter.ConvertFromString(artistas[5].Img);
					Label_6.Content = artistas[5].Nome;
					break;

				default:
					converter = new ImageSourceConverter();
					Image_1.Source = (ImageSource)converter.ConvertFromString(artistas[0].Img);
					Label_1.Content = artistas[0].Nome;
					converter = new ImageSourceConverter();
					Image_2.Source = (ImageSource)converter.ConvertFromString(artistas[1].Img);
					Label_2.Content = artistas[1].Nome;
					converter = new ImageSourceConverter();
					Image_3.Source = (ImageSource)converter.ConvertFromString(artistas[2].Img);
					Label_3.Content = artistas[2].Nome;
					converter = new ImageSourceConverter();
					Image_4.Source = (ImageSource)converter.ConvertFromString(artistas[3].Img);
					Label_4.Content = artistas[3].Nome;
					converter = new ImageSourceConverter();
					Image_5.Source = (ImageSource)converter.ConvertFromString(artistas[4].Img);
					Label_5.Content = artistas[4].Nome;
					converter = new ImageSourceConverter();
					Image_6.Source = (ImageSource)converter.ConvertFromString(artistas[5].Img);
					Label_6.Content = artistas[5].Nome;
					break;
			}
		}

		private void Label_1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[0].id);
				}
			}
		}

		private void Label_2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[1].id);
				}
			}
		}

		private void Label_3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[2].id);
				}
			}
		}

		private void Label_4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[3].id);
				}
			}
		}

		private void Label_5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[4].id);
				}
			}
		}

		private void Label_6_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[5].id);
				}
			}
		}

		private void Image_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[0].id);
				}
			}
		}

		private void Image_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[1].id);
				}
			}
		}

		private void Image_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[2].id);
				}
			}
		}

		private void Image_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[3].id);
				}
			}
		}

		private void Image_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[4].id);
				}
			}
		}

		private void Image_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					(window as MainWindow).ContentSwitch.Content = new ArtistUC(artistas[5].id);
				}
			}
		}
	}
}
