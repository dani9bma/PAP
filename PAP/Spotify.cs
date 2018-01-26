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

    public partial class Spotify : UserControl
    {
        private SpotifyWebAPI _spotify;
        private bool isAuthtenticated = false;

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

        public List<FullArtist> ProcurarArtistasSpotify(string procura)
        {
            List<FullArtist> artistas = new List<FullArtist>();
            if (isAuthtenticated)
            {
                artistas = _spotify.SearchItems(procura, SearchType.Artist, 50).Artists.Items;
                return artistas;
            }

            MessageBox.Show("Precisa de se autenticar no spotify antes de procurar pelo spotify");
            return artistas;
        }

        public List<Artista> ProcurarArtistas(string procura, int qtd = 5)
        {
            Sql sql = new Sql();
            List<Artista> artistas = new List<Artista>();
            Artista[] sqlArt = new Artista[qtd];
            sqlArt = sql.ProcurarArtistas(procura, qtd);

            for (int i = 0; i < sqlArt.Length; i++)
            {
                artistas.Add(sqlArt[i]);
            }

            return artistas;
        }

        public bool GetAutenticado()
        {
            return isAuthtenticated;
        }
    }
}