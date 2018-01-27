using System;
using System.Collections.Generic;
using MySql.Data;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;
using MySql.Data.MySqlClient;

namespace PAP
{
    public class Database
    {
        private MySql.Data.MySqlClient.MySqlConnection _conn;
        private string _myConnectionString;
        private const string _storageAccountName = "papmusic";
        private const string _storageAccountKey = "/JSUsLXx4IZOp4XSjYjp48uyEf4PV0TQQNrlPM0mpkKb4SpDmEpKkvGewS2QZ0k4DeNPoRg4vwXoWRPqkYYV1g==";
        private CloudFileDirectory rootDir;

        public Database()
        {
            //MySql
            _myConnectionString = "server=den1.mysql5.gear.host;uid=papmusic;" +
                                 "pwd=Og8c5uDo~6~7;database=papmusic";

            try
            {
                _conn = new MySql.Data.MySqlClient.MySqlConnection();
                _conn.ConnectionString = _myConnectionString;
                _conn.Open();
                Console.WriteLine("MySql Working");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Azure
            var StorageAccount = new CloudStorageAccount(new StorageCredentials(_storageAccountName, _storageAccountKey), false);
            var share = StorageAccount.CreateCloudFileClient().GetShareReference("tracks");
            rootDir = share.GetRootDirectoryReference();
        }

        public void DownloadFiles()
        {
            /*foreach (var fileItem in rootDir.ListFilesAndDirectories())
            {
                if (fileItem is CloudFile file)
                {
                    //var nomeMusica = file.Name.;
                }
            }*/

        }

        public void InserirArtistas(string nome, string img = "")
        {
            string sql = "INSERT INTO artistas (nome, img) VALUES ('" + nome + "', '" + img + "')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public void InserirMusicas(string nomeMusica, int codArtista)
        {
            string sql = "INSERT INTO musicas (id_artista, nome) VALUES ('" + codArtista + "', '" + nomeMusica + "\')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public Artista[] ProcurarArtistas(string nome, int qtd)
        {
            Artista[] artista = new Artista[qtd];
            string sql = "SELECT nome, img FROM artistas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            for (int i = 0; i < qtd; i++)
            {
                rdr.Read();
                if (!(art == rdr[0]))
                {
                    art = rdr[0].ToString();
                    artista[i].Nome = rdr[0].ToString();
                    artista[i].Img = rdr[1].ToString();
                }
                else
                {
                    artista[i].Nome = "";
                    artista[i].Img = "";
                }
            }
            rdr.Close();

            return artista;
        }

        public Artista ProcurarArtista(int cod)
        {
            Artista artista = new Artista();
            string sql = "SELECT nome, img FROM artistas WHERE id_artista = " + cod;
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            
            rdr.Read();
            if (art != rdr[0].ToString())
            {
                art = rdr[0].ToString();
                artista.Nome = rdr[0].ToString();
                artista.Img = rdr[1].ToString();
            }
            else
            {
                artista.Nome = "";
                artista.Img = "";
            }
            rdr.Close();

            return artista;
        }

        public Musica[] ProcurarMusicas(string nome, int qtd)
        {
            Musica[] musica = new Musica[qtd];
            List<Artista> artista = new List<Artista>();
            artista = GetTodosArtistas();
            string sql = "SELECT nome, id_artista FROM musicas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";

            if (rdr.Read())
            {
                for (int i = 0; i < qtd; i++)
                {
                    if (art != rdr[0].ToString())
                    {
                        var m = rdr[1].ToString();
                        art = rdr[0].ToString();
                        musica[i].Nome = art;
                        musica[i].artista = artista[int.Parse(m) - 1];
                    }
                    else
                    {
                        musica[i].Nome = "";
                        musica[i].artista = new Artista();
                    }
                }
            }

            return musica;
        }

        public int GetCodigoArtista(string nome)
        {
            string sql = "SELECT id_artista FROM artistas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            int cod = int.Parse(rdr[0].ToString());
            rdr.Close();
            return cod;
        }

        public List<Artista> GetTodosArtistas()
        {
            List<Artista> artistas = new List<Artista>();
            string sql = "SELECT nome, img FROM artistas";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int i = 0;
            while (rdr.Read())
            {
                if (!(art == rdr[0]))
                {
                    art = rdr[0].ToString();
                    Artista artista = new Artista();
                    artista.Nome = rdr[0].ToString();
                    artista.Img = rdr[1].ToString();
                    artistas.Add(artista);
                }
                else
                {
                    Artista artista = new Artista();
                    artista.Nome = rdr[0].ToString();
                    artista.Img = rdr[1].ToString();
                    artistas.Add(artista);
                }

                i++;
            }
            rdr.Close();

            return artistas;
        }
    }
}