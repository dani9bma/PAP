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
	/// Interaction logic for MusicUC.xaml
	/// </summary>
	public partial class MusicUC : UserControl
	{
		private List<Musica> Musicas = new List<Musica>();

		public MusicUC()
		{
			InitializeComponent();
			InitWindow();
		}

		private void InitWindow()
		{
			int cod = LoginInfo.id;

			List<Musica> codMusicas = Global.sql.GetMusicasFavoritas(cod);

			for (int i = 0; i < codMusicas.Count; i++)
			{
				Musica musica = Global.sql.ProcurarMusica(codMusicas[i].id);
				Artista artista = Global.sql.ProcurarArtista(musica.artista.id);
				musica.artista = artista;
				Musicas.Add(musica);

				FavoriteMusicsLB.Items.Add(Musicas[i].Nome);
			}
		}

		private async void FavoriteMusicsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int pos = FavoriteMusicsLB.SelectedIndex;

			MediaElement player = new MediaElement();

			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(MainWindow))
				{
					player = (window as MainWindow).MediaPlayer;
				}
			}

			player.Source = null;
			Uri source = null;
			try
			{
				await Global.sql.DownloadFiles(Musicas[pos].Nome);
				source = new Uri(Path.GetTempPath() + "music.mp4");
			}
			catch (Exception ex)
			{
				MessageBox.Show("A musica que selecionou nao poderá ser ouvida");
			}

			player.Source = source;
			player.Play();

		}
	}
}
