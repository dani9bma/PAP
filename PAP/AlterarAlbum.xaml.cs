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
	/// Interaction logic for AlterarAlbum.xaml
	/// </summary>
	public partial class AlterarAlbum : UserControl
	{
		private int _cod;
		private List<Artista> _artists = new List<Artista>();
		private List<Musica> musicas = Global.sql.GetTodasMusicas();
		Album album = new Album();

		public AlterarAlbum(int cod)
		{
			InitializeComponent();
			Init(cod);
		}

		private void Init(int cod)
		{
			_cod = cod;
			album = Global.sql.ProcurarAlbumMusicas(_cod);
			idTxt.Text = album.id.ToString();
			NameTxt.Text = album.Nome;

			for(int i = 0; i < album.Musicas.Count; i++)
			{
				Musica musica = Global.sql.ProcurarMusica(album.Musicas[i].id);
				OldTrack.Items.Add(musica.Nome);
			}
			OldTrack.Items.Add("Adicionar Nova Musica");

			for(int i = 0; i < musicas.Count; i++)
			{
				NewTrack.Items.Add(musicas[i].Nome);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Global.sql.AlterarAlbum(_cod, NameTxt.Text);
			foreach (Window window in Application.Current.Windows)
			{
				if (window.GetType() == typeof(AdminMain))
				{
					(window as AdminMain).ContentSwitch.Content = new AlbumAdminUC();
				}
			}
		}

		private void AlterarMusica_Click(object sender, RoutedEventArgs e)
		{
			if (OldTrack.SelectedIndex >= 0 && NewTrack.SelectedIndex >= 0)
			{
				if (OldTrack.SelectedItem.ToString() == "Adicionar Nova Musica")
				{
					int cod = musicas[NewTrack.SelectedIndex].id;
					string nome = musicas[NewTrack.SelectedIndex].Nome;

					Global.sql.AdicionarMusicaAlbum(cod, _cod);

					OldTrack.Items.Add(nome);
				}
				else
				{
					int Antcod = album.Musicas[OldTrack.SelectedIndex].id;
					int newcod = musicas[NewTrack.SelectedIndex].id;
					string newNome = musicas[NewTrack.SelectedIndex].Nome;

					Global.sql.SubstituirMusicaAlbum(Antcod, newcod, _cod);

					OldTrack.Items[OldTrack.SelectedIndex] = newNome;
				}
			}
		}

		//Eliminar Musica
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if(OldTrack.SelectedIndex >= 0)
			{
				int Antcod = album.Musicas[OldTrack.SelectedIndex].id;
				Global.sql.DeleteMusicaAlbum(_cod, Antcod);
				OldTrack.Items.RemoveAt(OldTrack.SelectedIndex);
			}
		}
	}
}
