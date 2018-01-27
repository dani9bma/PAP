using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private bool _spotifyArt = false;

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

            _tracks = _spotify.ProcurarMusicas(artistaTB.Text, 10);
            for (int i = 0; i < _tracks.Count; i++)
            {
                artistasLB.Items.Add(_tracks[i].Nome);
                //artistaImgPB.ImageLocation = _artists[i].Img;
                Console.WriteLine(_tracks[i].Nome);
                Console.WriteLine(_tracks[i].artista.Nome);
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
            playMP.URL = _sql.DownloadFiles(_tracks[pos].Nome, "Drake");
            Console.WriteLine(playMP.URL);
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
    }
}
