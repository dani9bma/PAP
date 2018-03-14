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

			List<Musica> musicas = Global._sql.GetTodasMusicasArtista(id_artista);
			for(int i = 0; i < musicas.Count; i++)
			{
				ArtistTracksLB.Items.Add(musicas[i].Nome);
			}
		}
	}
}
