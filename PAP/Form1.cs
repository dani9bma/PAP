﻿using System;
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
            var tracks = _spotify.ProcurarMusicaSpotify("Logic");
            for (int i = 0; i < tracks.Count; i++)
            {
                Console.WriteLine(tracks[i].Name);
                Console.WriteLine(tracks[i].Artists[0].Name);
                //TODO: Pesquisar musicas por artista no spotify e adicionar a base de dados
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            _artists = _spotify.ProcurarArtistas(artistaTB.Text, 10);
            artistasLB.Items.Clear();
            for (int i = 0; i < _artists.Count; i++)
            {
                artistasLB.Items.Add(_artists[i].Nome);
                artistaImgPB.ImageLocation = _artists[i].Img;
            }

            _spotifyArt = false;
        }

        private void searchSpoBtn_Click(object sender, EventArgs e)
        {
            _artistsSpotify = _spotify.ProcurarArtistasSpotify(artistaTB.Text);
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

            if (_spotifyArt)
                artistaImgPB.ImageLocation = _artistsSpotify[pos].Images[0].Url;
            else
                artistaImgPB.ImageLocation = _artists[pos].Img;
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


    }
}
