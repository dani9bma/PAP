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
        public string nome;
        public string img;
    }

    public partial class Spotify : UserControl
    {
        private SpotifyWebAPI _spotify;
        private PrivateProfile _profile;

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
            InitialSetup();
        }

        public List<FullArtist> ProcurarArtistas(string procura)
        {
            return _spotify.SearchItems(procura, SearchType.Artist, 50).Artists.Items;
        }
    }
}