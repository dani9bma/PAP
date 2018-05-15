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
	/// Interaction logic for PlaylistsUC.xaml
	/// </summary>
	public partial class PlaylistsUC : UserControl
	{
		List<Musica> musicas = new List<Musica>();
		int id_playlist = 0;

		public PlaylistsUC(int id)
		{
			InitializeComponent();
			id_playlist = id;
			InitWindow(id);
		}

		private void InitWindow(int id)
		{
			musicas = Global.sql.GetPlaylistsMusicas(id);
			
			for(int i = 0; i < musicas.Count; i++)
			{
				TracksLB.Items.Add(musicas[i].Nome);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			int SI = TracksLB.SelectedIndex;
			int id_musica = musicas[SI].id;
			Global.sql.DeleteMusicaPlaylist(id_playlist, id_musica);
		}
	}
}
