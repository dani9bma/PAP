using System;
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
            string sql = "INSERT INTO musicas (id_artista, nome) VALUES ('" + codArtista + "', '" + nomeMusica + "')";
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
    }
}