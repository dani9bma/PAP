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
	/// Interaction logic for ArtistWindow.xaml
	/// </summary>
	public partial class ArtistWindow : Window
	{

		List<Musica> musicas = new List<Musica>();

		public ArtistWindow(int id_artista)
		{
			InitializeComponent();
			InitPage(id_artista);
		}

		private void InitPage(int id_artista)
		{
			Artista artista = Global._sql.GetArtistaCodigo(id_artista);
			var converter = new ImageSourceConverter();
			ArtistPicture.Source = (ImageSource)converter.ConvertFromString(artista.Img);
			ArtistName.Content = artista.Nome;

			musicas = Global._sql.GetTodasMusicasArtista(id_artista);
			for(int i = 0; i < musicas.Count; i++)
			{
				ArtistTracksLB.Items.Add(musicas[i].Nome);
			}
		}

		private void ArtistTracksLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = ArtistTracksLB.SelectedIndex;

			Console.WriteLine(musicas[pos].Nome);
			Console.WriteLine(musicas[pos].artista.Nome);

			Player.Source = null;

			Uri source = new Uri(Global._sql.DownloadFiles(musicas[pos].Nome, musicas[pos].artista.Nome));
			
			Player.Source = source;
			Player.Play();
			Console.WriteLine(Player.Source);
		}

		private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Player.Volume = VolumeSlider.Value;
		}

		private void StopButton_Click(object sender, RoutedEventArgs e)
		{
			if (Player.CanPause)
				Player.Pause();
		}

		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			Player.Play();
		}
	}
}
