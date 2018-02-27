using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SpotifyAPI.Web.Models;

namespace PAP
{

	public partial class Form1 : MetroForm
    {
        private Spotify _spotify = new Spotify();
        private Database _sql = new Database();
        private List<FullArtist> _artistsSpotify = new List<FullArtist>();
        private List<Artista> _artists = new List<Artista>();
        private List<Musica> _tracks = new List<Musica>();
        private List<Album> _albums = new List<Album>();
        private bool _spotifyArt = false;
		private List<Musica> _musicas = new List<Musica>();

        public Form1()
        {
	        InitializeComponent();
        }

        private void authButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>_spotify.RunAuthentication());
        }

        private void mysql_Click(object sender, EventArgs e)
        {
            List<FullTrack> tracks = new List<FullTrack>();
            List<Musica> musicas = new List<Musica>();
            musicas = _sql.GetTodasMusicas();
            for (int i = 0; i < musicas.Count; i++)
            {
                tracks = _spotify.ProcurarMusicaSpotify(musicas[i].Nome, 1);
                if (tracks.Count > 0)
                {
                    int cod = _sql.GetCodigoAlbum(tracks[0].Album.Name);
                    if (cod == -1)
                    {
                        cod = _sql.GetTotalAlbums();
                        cod++;
                        _sql.InserirAlbum(cod, tracks[0].Album.Name, musicas[i].id);
                        Console.WriteLine(tracks[0].Album.Name);
                    }
                    else
                    {
                        _sql.InserirAlbum(cod, tracks[0].Album.Name, musicas[i].id);
                        Console.WriteLine(tracks[0].Album.Name);
                    }
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
			/*_artists = _spotify.ProcurarArtistas(artistaTB.Text, 10);
            artistasLB.Items.Clear();
            for (int i = 0; i < _artists.Count; i++)
            {
                artistasLB.Items.Add(_artists[i].Nome);
                artistaImgPB.ImageLocation = _artists[i].Img;
            }

            _spotifyArt = false;*/

			// TODO: Tirar spotifyArt
			_tracks = _spotify.ProcurarMusicas(artistaTB.Text, 10);
			artistasLB.Items.Clear();

            List<Musica> m = _spotify.ProcurarMusicasPorArtista(artistaTB.Text, 5);
            for (int i = 0; i < m.Count; i++)
            {
                _tracks.Add(m[i]);
            }

            for (int i = 0; i < _tracks.Count; i++)
            {
                artistasLB.Items.Add(_tracks[i].Nome);
                Console.WriteLine(_tracks[i].Nome);
                Console.WriteLine(_tracks[i].artista.Nome);
            }

            _spotifyArt = false;

            _artists = _spotify.ProcurarArtistas(artistaTB.Text, 10);
            musicasLB.Items.Clear();
            for (int i = 0; i < _artists.Count; i++)
            {
                musicasLB.Items.Add(_artists[i].Nome);
                Console.WriteLine(_artists[i].Nome);
            }
            _spotifyArt = false;

            _albums = _spotify.ProcurarAlbums(artistaTB.Text, 10);
            albumsLB.Items.Clear();
            for (int i = 0; i < _albums.Count; i++)
            {
                albumsLB.Items.Add(_albums[i].Nome);
                Console.WriteLine(_albums[i].Nome);
            }

            _spotifyArt = false;
        }

        private void searchSpoBtn_Click(object sender, EventArgs e)
        {
            _artistsSpotify = _spotify.ProcurarArtistasSpotify(artistaTB.Text, 5);
            artistasLB.Items.Clear();
            for (int i = 0; i < _artistsSpotify.Count; i++)
            {
                artistasLB.Items.Add(_artistsSpotify[i].Name);
                artistaImgPB.ImageLocation = _artistsSpotify[i].Images[0].Url;
            }
            _spotifyArt = true;
        }

        private void artistasLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pos = artistasLB.SelectedIndex;

            /*if (_spotifyArt)
                artistaImgPB.ImageLocation = _artistsSpotify[pos].Images[0].Url;
            else
                artistaImgPB.ImageLocation = _artists[pos].Img;*/
            Console.WriteLine(_tracks[pos].Nome);
            Console.WriteLine(_tracks[pos].artista.Nome);
            playMP.URL = "";
            playMP.URL = _sql.DownloadFiles(_tracks[pos].Nome, _tracks[pos].artista.Nome);
            Console.WriteLine(playMP.URL);

			_sql.InserirMusicasFavoritas(_tracks[pos].id, LoginInfo.id);
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            _sql.RegistarUtilizador(usernameTB.Text, passwordTB.Text);
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
	        if (LoginInfo.username != "")
		        MessageBox.Show("Voce ja esta logado");
	        else
		        _sql.LoginUtilizador(usernameTB.Text, passwordTB.Text);

        }

		private void musicasLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = musicasLB.SelectedIndex;
			
            artistaImgPB.ImageLocation = _artists[pos].Img;

			_sql.InserirArtistasFavoritos(_artists[pos].id, LoginInfo.id);
		}

		private void albumsLB_SelectedIndexChanged(object sender, EventArgs e)
		{
			int pos = albumsLB.SelectedIndex;

			_sql.InserirAlbumsFavoritos(_albums[pos].id, LoginInfo.id);
		}
	}
}



#region Codigo para inserir na base de dados pelo spotify
/*Dictionary<string, string> artistas = new Dictionary<string, string>();
   Dictionary<string, string> artistasPeli = new Dictionary<string, string>();
   for (int i = 97; i < 123; i++)
   {
	   var chara = (char)i + "*";
	   Console.WriteLine(chara);
	   var art = _spotify.ProcurarArtistas(chara);
	   for (int j = 0; j < art.Count; j++)
	   {
		   if (art[j].Name.Contains("'"))
		   {
			   if (art[j].Images != null)
				   try
				   {
					   artistasPeli.Add(art[j].Name, art[j].Images[0].Url);
				   }
				   catch (Exception exception)
				   {
					   Console.WriteLine(exception);
				   }

			   else
				   try
				   {
					   artistasPeli.Add(art[j].Name, "");
				   }
				   catch (Exception exception)
				   {
					   Console.WriteLine(exception);
				   }
		   }
		   else
		   {
			   if(art[j].Images != null)
				   try
				   {
					   artistas.Add(art[j].Name, art[j].Images[0].Url);
				   }
				   catch (Exception exception)
				   {
					   Console.WriteLine(exception);
				   }
			   else
				   try
				   {
					   artistas.Add(art[j].Name, "");
				   }
				   catch (Exception exception)
				   {
					   Console.WriteLine(exception);
				   }
		   }

	   }

   }

   foreach (KeyValuePair<string, string> kvp in artistasPeli)
   {
	   Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
   }

   foreach (KeyValuePair<string, string> kvp in artistas)
   {
	   _sql.InserirArtistas(kvp.Key, kvp.Value);
   }*/


#endregion

#region Codigo para inserir musicas na base de dados pelo spotify
/*List<Artista> artistas = new List<Artista>();

artistas = _sql.GetTodosArtistas();
for (int i = 0; i<artistas.Count; i++)
{
	var tracks = _spotify.ProcurarMusicaSpotify(artistas[i].Nome, 10);
	for (int j = 0; j<tracks.Count; j++)
	{
		if (tracks[j].Artists[0].Name == artistas[i].Nome)
		{
			if (!tracks[j].Name.Contains("'"))
			{
				//Console.WriteLine(artistas[i].Nome);
				_sql.InserirMusicas(tracks[j].Name, _sql.GetCodigoArtista(artistas[i].Nome));
			}
			else
			{
				var t = tracks[j].Name.Replace("'", " ");
_sql.InserirMusicas(t, _sql.GetCodigoArtista(artistas[i].Nome));
			}
		}
	} 
}

Console.WriteLine("Finished");*/
#endregion
