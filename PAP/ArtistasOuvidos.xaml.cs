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

			for(int i = 0; i < artistas.Count; i++)
			{
				switch(i)
				{
					case 1:

						break;

					case 2:

						break;

					case 3:

						break;

					case 4:

						break;

					case 5:

						break;

					case 6:

						break;
				}
			}
		}
	}
}
