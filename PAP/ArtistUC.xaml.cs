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
using System.IO;

namespace PAP
{
	/// <summary>
	/// Interaction logic for ArtistUC.xaml
	/// </summary>
	public partial class ArtistUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();

		public ArtistUC(int cod)
		{
			InitializeComponent();
			InitPage(cod);
		}

		private void InitPage(int id_artista)
		{
			Artista artista = Global.sql.ProcurarArtista(id_artista);
			var converter = new ImageSourceConverter();
			ArtistPicture.Source = (ImageSource)converter.ConvertFromString(artista.Img);
			ArtistName.Content = artista.Nome;

			musicas = Global.sql.GetTodasMusicasArtista(id_artista);
			for (int i = 0; i < musicas.Count; i++)
			{
				ArtistTracksLB.Items.Add(musicas[i].Nome);
			}
		}

		private async void ArtistTracksLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = ArtistTracksLB.SelectedIndex;

			Console.WriteLine(musicas[pos].Nome);
			Console.WriteLine(musicas[pos].artista.Nome);

			MediaElement Player = new MediaElement();

			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					Player = (window as MainWindow).MediaPlayer;
				}
			}

			Player.Source = null;
			Uri source = null;
			try
			{
				await Global.sql.DownloadFiles(musicas[pos].Nome);
				source = new Uri(Path.GetTempPath() + "music.mp4");
			}
			catch (Exception ex)
			{
				MessageBox.Show("A musica que selecionou nao poderá ser ouvida");
			}

			if (source != null)
				Player.Source = source;
			else
				Player.Source = null;
			Player.Play();
			Console.WriteLine(Player.Source);
		}
	}
}
