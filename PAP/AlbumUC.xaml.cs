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
	/// Interaction logic for AlbumUC.xaml
	/// </summary>
	public partial class AlbumUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();

		public AlbumUC(int cod)
		{
			InitializeComponent();
			InitPage(cod);
		}

		private void InitPage(int id_album)
		{
			Album album = Global.sql.ProcurarAlbumMusicas(id_album);

			AlbumName.Content = album.Nome;

			for(int i = 0; i < album.Musicas.Count; i++)
			{
				Musica musica = new Musica();
				musica = Global.sql.ProcurarMusica(album.Musicas[i].id);
				musicas.Add(musica);
			}
	
			for (int i = 0; i < musicas.Count; i++)
			{
				AlbumTracksLB.Items.Add(musicas[i].Nome);
			}
		}

		private async void AlbumTracksLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = AlbumTracksLB.SelectedIndex;

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
