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
        private Sql _sql = new Sql();

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


        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            var artistas = _spotify.ProcurarArtistas("Lo");
            for (int i = 0; i < artistas.Count; i++)
            {
                Console.WriteLine(artistas[i].nome);
                Console.WriteLine(artistas[i].img);
            }
        }
    }
}
