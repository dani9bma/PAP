﻿using System;
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

        public string DownloadFiles(string musica, string artista)
        {
            foreach (var fileItem in rootDir.ListFilesAndDirectories())
            {
                if (fileItem is CloudFile file)
                {
                    var nomeMusica = file.Name;
                    if (nomeMusica.Contains(musica) && nomeMusica.Contains(artista))
                    {
                        Console.WriteLine(nomeMusica);
                        file.DownloadToFile("C:/music.mp3", FileMode.Create);
                        return "C:/music.mp3";
                    }
                }
            }

            return "";
        }

        public void AzureToMySql()
        {
            var artists = GetTodosArtistas();

            foreach (var fileItem in rootDir.ListFilesAndDirectories())
            {
                if (fileItem is CloudFile file)
                {
                    var nomeMusica = file.Name;
                    for (int i = 0; i < artists.Count; i++)
                    {
                        if (nomeMusica.Contains(artists[i].Nome))
                        {
                            var nome = nomeMusica.Replace(artists[i].Nome, "");
                            nome = nome.Replace(".mp3", "");
                            if (nome.Contains("Lyrics"))
                                nome = nome.Replace("Lyrics", "");
                            if (nome.Contains("Video"))
                                nome = nome.Replace("Video", "");
                            //if (nome.Contains("-"))
                            //    nome = nome.Replace("-", "");
                            //if (nome.Contains("Audio") || nome.Contains("audio"))
                            //    nome = nome.Replace("Audio", "");
                            //if (nome.Contains("Official"))
                            //    nome = nome.Replace("Official", "");
                            //if (nome.Contains("Explicit"))
                            //    nome = nome.Replace("-", "");
                            //if (nome.Contains("["))
                            //    nome = nome.Replace("[", "");
                            //if (nome.Contains("]"))
                            //    nome = nome.Replace("]", "");
                            //if (nome.Contains("Music"))
                            //    nome = nome.Replace("Music", "");
                            //if (nome.Contains("'"))
                            //    nome = nome.Replace("'", " ");
                            //if (nome.Contains("&"))
                            //    nome = nome.Replace("&", " ");

                            var cod = GetCodigoArtista(artists[i].Nome);
                            InserirMusicas(nome, cod);
                        }
                    }
                }
            }

            Console.WriteLine("Done Azure To Mysql");
        }

        public void InserirArtistas(string nome, string img = "")
        {
            Artista[] art = ProcurarArtistas(nome, 1);
            if (nome.Contains("'"))
                nome = nome.Replace("'", " ");
            if (img == art[0].Img)
                return;
            string sql = "INSERT INTO artistas (nome, img) VALUES ('" + nome + "', '" + img + "')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public void InserirAlbum(int cod, string nome, int codMusica)
        {
            if (nome.Contains("'"))
                nome = nome.Replace("'", " ");
            string sql = "INSERT INTO albums (id_album, nome, id_musica) VALUES (" + cod + ", '" + nome + "', "+ codMusica +")";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public void InserirMusicas(string nomeMusica, int codArtista)
        {
            if (nomeMusica.Contains("'"))
                nomeMusica = nomeMusica.Replace("'", " ");
            string sql = "INSERT INTO musicas (id_artista, nome) VALUES ('" + codArtista + "', '" + nomeMusica + "\')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        //Retorna Artista procurando pelo nome
        public Artista[] ProcurarArtistas(string nome, int qtd)
        {
	        //TODO: Verificar se existe na base dados (fazer mesma coisa que no ProcurarMusicas)
			Artista[] artista = new Artista[qtd];
            string sql = "SELECT nome, img FROM artistas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            for (int i = 0; i < qtd; i++)
            {
                if (!rdr.Read())
                    return artista;
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

        //Retorna Artista procurando pelo codigo
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

        public List<Musica> ProcurarMusicasPorArtista(int cod, int qtd)
        {
            Musica[] musica = new Musica[qtd];
            List<Artista> artista = new List<Artista>();
            artista = GetTodosArtistas();
            string sql = "SELECT nome, id_artista FROM musicas WHERE id_artista = " + cod;
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int artId = -1;

            for (int i = 0; i < qtd; i++)
            {
                if (rdr.Read())
                {
                    if (art != rdr[0].ToString() || artId != int.Parse(rdr[1].ToString()))
                    {
                        var m = rdr[1].ToString();
                        art = rdr[0].ToString();
                        artId = int.Parse(rdr[1].ToString());
                        musica[i].Nome = art;
                        musica[i].artista = artista[int.Parse(m) - 1];
                    }
                    else
                    {
                        musica[i].Nome = "";
                        musica[i].artista = new Artista();
                    }
                }
                else
                {
                    musica[i].Nome = "";
                    musica[i].artista = new Artista();
                }
            }

            rdr.Close();

            List<Musica> mu = new List<Musica>();
            for (int i = 0; i < musica.Length; i++)
            {
                if (musica[i].Nome != "")
                {
                    mu.Add(musica[i]);
                }
            }

            if (mu.Count > 0)
            {
                return mu;
            }

            return mu;
        }

        public List<Musica> ProcurarMusicas(string nome, int qtd)
        {
            //TODO: Na procura, procurar mostra musicas, artistas e albuns
            //TODO: Rever codigo 
            Musica[] musica = new Musica[qtd];
            List<Artista> artista = new List<Artista>();
            artista = GetTodosArtistas();
            string sql = "SELECT nome, id_artista FROM musicas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int artId = -1;

            for (int i = 0; i < qtd; i++)
            {
                if (rdr.Read())
                {
                    if (art != rdr[0].ToString() || artId != int.Parse(rdr[1].ToString()))
                    {
                        var m = rdr[1].ToString();
                        art = rdr[0].ToString();
                        artId = int.Parse(rdr[1].ToString());
                        musica[i].Nome = art;
                        musica[i].artista = artista[int.Parse(m) - 1];
                    }
                    else
                    {
                        musica[i].Nome = "";
                        musica[i].artista = new Artista();
                    }
                }
                else
                {
                    musica[i].Nome = "";
                    musica[i].artista = new Artista();
                }
            }
			
            rdr.Close();

			List<Musica> mu = new List<Musica>();
	        for (int i = 0; i < musica.Length; i++)
	        {
		        if (musica[i].Nome != "")
		        {
			        mu.Add(musica[i]);
		        }
	        }

	        if (mu.Count > 0)
	        {
		        return mu;
			}

	        MessageBox.Show("A musica que tentou pesquisar não existe na nossa base de dados, por favor entre aqui (LINK) e contribua com a música que deseja");
            //TODO: Quando nao encontra musica abrir outra janela com um formulario com o nome da musica, link da musica, artista(<select>) e um botao de "Enviar", clicado no botao este deverá fazer o download da musica e adicionala na base de dados
	        return mu;
        }

        public List<Album> ProcurarAlbums(string nome, int qtd)
        {
            Album[] album = new Album[qtd];
            string sql = "SELECT nome FROM albums WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int artId = -1;

            for (int i = 0; i < qtd; i++)
            {
                if (rdr.Read())
                {
                    if (art != rdr[0].ToString())
                    {
                        art = rdr[0].ToString();
                        album[i].Nome = art;
                    }
                    else
                    {
                        album[i].Nome = "";
                    }
                }
                else
                {
                    album[i].Nome = "";
                }
            }

            rdr.Close();

            List<Album> albums = new List<Album>();
            for (int i = 0; i < album.Length; i++)
            {
                if (album[i].Nome != "")
                {
                    albums.Add(album[i]);
                }
            }

            if (albums.Count > 0)
            {
                return albums;
            }

            MessageBox.Show("O album que tentou pesquisar não existe na nossa base de dados, por favor entre aqui (LINK) e contribua com a música que deseja");
            //TODO: Quando nao encontra musica abrir outra janela com um formulario com o nome da musica, link da musica, artista(<select>) e um botao de "Enviar", clicado no botao este deverá fazer o download da musica e adicionala na base de dados
            return albums;
        }

        public int GetCodigoArtista(string nome)
        {
			//TODO: Verificar se existe na base dados (fazer mesma coisa que no ProcurarMusicas)
            if (nome.Contains("'"))
                nome = nome.Replace("'", " ");
            string sql = "SELECT id_artista FROM artistas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int cod = int.Parse(rdr[0].ToString());
                rdr.Close();
                return cod;
            }

            rdr.Close();

            MessageBox.Show("O artista que procurou nao existe na base de dados");
            return -1;
        }

        public int GetTotalAlbums()
        {
            string sql = "SELECT COUNT(id_album) FROM albums";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int cod = int.Parse(rdr[0].ToString());
                rdr.Close();
                return cod;
            }

            rdr.Close();

            return -1;
        }

        public int GetCodigoAlbum(string nome)
        {
            if (nome.Contains("'"))
                nome = nome.Replace("'", " ");
            string sql = "SELECT id_album FROM albums WHERE nome LIKE '" + nome + "'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                int cod = int.Parse(rdr[0].ToString());
                rdr.Close();
                return cod;
            }

            rdr.Close();

            return -1;
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

        public List<Musica> GetTodasMusicas()
        {
            List<Musica> musicas = new List<Musica>();
            string sql = "SELECT id_musica, nome FROM musicas";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int i = 0;
            while (rdr.Read())
            {
                if (!(art == rdr[0]))
                {
                    art = rdr[0].ToString();
                    Musica musica = new Musica {id = int.Parse(rdr[0].ToString()), Nome = rdr[1].ToString()};
                    musicas.Add(musica);
                }
                else
                {
                    Musica musica = new Musica {Nome = rdr[0].ToString()};
                    musicas.Add(musica);
                }

                i++;
            }
            rdr.Close();

            return musicas;
        }
    }
}