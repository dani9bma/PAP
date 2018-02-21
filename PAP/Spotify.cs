using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace PAP
{
    public struct Artista
    {
        public string Nome;
        public string Img;
    }

    public struct Musica
    {
        public int id;
        public string Nome;
        public Artista artista;
    }

    public struct Album
    {
        public int id;
        public string Nome;
        public List<Musica> Musicas;
    }

    public partial class Spotify : UserControl
    {
        private SpotifyWebAPI _spotify;
        private bool isAuthtenticated = false;
	    private Database _sql = new Database();

		public Spotify()
        {
        }

        private async void InitialSetup()
        {
            if (!InvokeRequired) return;
            Invoke(new Action(InitialSetup));
            return;
        }

        public async void RunAuthentication()
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "63ac5440d56e4958bacadaf58f993a19",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);
            try
            {
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (_spotify == null)
                return;

            Console.WriteLine("Spotify Working");
            isAuthtenticated = true;
            InitialSetup();
        }
        
        public List<FullArtist> ProcurarArtistasSpotify(string procura, int qtd)
        {
            List<FullArtist> artistas = new List<FullArtist>();
            if (isAuthtenticated)
            {
                artistas = _spotify.SearchItems(procura, SearchType.Artist, qtd, 0, "US").Artists.Items;
                return artistas;
            }

            MessageBox.Show("Precisa de se autenticar no spotify antes de procurar pelo spotify");
            return artistas;
        }

        public List<FullTrack> ProcurarMusicaSpotify(string procura, int qtd)
        {
            List<FullTrack> musicas = new List<FullTrack>();
            if (isAuthtenticated)
            {
                try
                {
                    musicas = _spotify.SearchItems(procura, SearchType.Track, qtd).Tracks.Items;
                    return musicas;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }

            MessageBox.Show("Precisa de se autenticar no spotify antes de procurar pelo spotify");
            return musicas;
        }

        public List<Artista> ProcurarArtistas(string procura, int qtd = 5)
        {
            List<Artista> artistas = new List<Artista>();
            List<Artista> sqlArt = _sql.ProcurarArtistas(procura, qtd);

            for (int i = 0; i < sqlArt.Count; i++)
            {
                artistas.Add(sqlArt[i]);
            }

            return artistas;
        }

        public List<Musica> ProcurarMusicas(string procura, int qtd)
        {
            
            List<Musica> musicas = new List<Musica>();
            musicas = _sql.ProcurarMusicas(procura, qtd);

            return musicas;
        }

        public List<Album> ProcurarAlbums(string procura, int qtd)
        {
            List<Album> albums = new List<Album>();
            albums = _sql.ProcurarAlbums(procura, qtd);

            return albums;
        }

        public List<Musica> ProcurarMusicasPorArtista(string procura, int qtd)
        {
            var artista = _sql.GetCodigoArtista(procura);
            List<Musica> musicas = new List<Musica>();
            musicas = _sql.ProcurarMusicasPorArtista(artista, qtd);

            return musicas;
        }

        public bool GetAutenticado()
        {
            return isAuthtenticated;
        }
    }
}