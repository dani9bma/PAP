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

		public PlaylistsUC(int id)
		{
			InitializeComponent();
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
	}
}
