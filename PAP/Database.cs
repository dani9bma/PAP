﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;
using MediaToolkit;
using MediaToolkit.Model;
using YoutubeSearch;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;
using System.Threading.Tasks;

namespace PAP
{

	public struct LoginInfo
	{
		public static string username = "";
		public static int id = -1;
	}

	public class User
	{
		public string username;
		public int id;
		public string password;
		public string nome;
		public string email;
	}

	public class ArtistasFavoritos
	{
		public int id_user;
		public int id_artista;
		public string user;
		public string artista;
	}

	public class MusicasFavoritas
	{
		public int id_user;
		public int id_musica;
		public string user;
		public string musica;
	}

	public class AlbumsFavoritos
	{
		public int id_user;
		public int id_album;
		public string user;
		public string album;
	}

	public class PlaylistFavoritas
	{
		public int id_user;
		public string user;
		public string playlist;
	}

	public struct Playlist
	{
		public int id;
		public int id_user;
		public List<Musica> musicas;
		public string nome;
	}

	public class Database
    {
        private MySql.Data.MySqlClient.MySqlConnection _conn;
        private string _myConnectionString;
        private const string _storageAccountName = "papmusic";
        private const string _storageAccountKey = "/JSUsLXx4IZOp4XSjYjp48uyEf4PV0TQQNrlPM0mpkKb4SpDmEpKkvGewS2QZ0k4DeNPoRg4vwXoWRPqkYYV1g==";

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

        }

		public async Task DownloadFiles(string musica)
        {
			//Youtube search
			VideoSearch items = new VideoSearch();
			var videos = items.SearchQuery(musica, 1);
			musica = videos[0].Url.Replace("http://www.youtube.com/watch?v=", "");

			var client = new YoutubeClient();
			var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(musica);

			var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
			var ext = streamInfo.Container.GetFileExtension();

			if(ext == "webm")
			{
				MessageBox.Show("Nao foi possivel ouvir a musica que pediu");
				File.Delete(Path.GetTempPath() + $"music.mp4");
			}
			else
			{
				if (File.Exists(Path.GetTempPath() + $"music.{ext}"))
					File.Delete(Path.GetTempPath() + $"music.{ext}");

				await client.DownloadMediaStreamAsync(streamInfo, Path.GetTempPath() + $"music.{ext}");
			}
		}

        public void AzureToMySql()
        {
            var artists = GetTodosArtistas();

            Console.WriteLine("Done Azure To Mysql");
        }

        public void InserirArtistas(string nome, string img = "")
        {
            List<Artista> art = ProcurarArtistas(nome, 1);
            if (nome.Contains("'"))
                nome = nome.Replace("'", " ");
			if(art.Count > 0)
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

		public void InserirAlbum(int cod, string nome, int codMusica, int codArtista)
		{
			if (nome.Contains("'"))
				nome = nome.Replace("'", " ");
			string sql = "INSERT INTO albums (id_album, nome, id_musica, id_artista) VALUES (" + cod + ", '" + nome + "', " + codMusica + ", " + codArtista + ")";
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

	    public void InserirMusicasFavoritas(int id_musica, int id_user)
	    {
			string sql = "INSERT INTO musicas_favoritas (id_musica, id_user) VALUES (" + id_musica+ ", " + id_user + ")";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void InserirAlbumsFavoritos(int id_album, int id_user)
		{
			string sql = "INSERT INTO albums_favoritos (id_album, id_user) VALUES (" + id_album + ", " + id_user + ")";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void InserirArtistasFavoritos(int id_artista, int id_user)
	    {
			string sql = "INSERT INTO artistas_favoritos (id_artista, id_user) VALUES (" + id_artista + ", " + id_user + ")";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void RemoverMusicasFavoritas(int id_musica, int id_user)
		{
			//TODO: Verificar se artista ja foi adicionado aos favoritos
			string sql = "DELETE FROM musicas_favoritas WHERE id_musica = " + id_musica + " AND id_user = " + id_user;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void RemoverAlbumsFavoritos(int id_album, int id_user)
		{
			//TODO: Verificar se artista ja foi adicionado aos favoritos
			string sql = "DELETE FROM albums_favoritos WHERE id_album = " + id_album + " AND id_user = " + id_user;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void RemoverArtistasFavoritos(int id_artista, int id_user)
		{
			//TODO: Verificar se artista ja foi adicionado aos favoritos
			string sql = "DELETE FROM artistas_favoritos WHERE id_artista = " + id_artista + " AND id_user = " + id_user;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void InserirPlaylist(string nome, int id_musica)
		{
			int id = GetPlaylistByNome(nome);

			if (id != -1)
			{
				string sql = "INSERT INTO playlists (id_playlist, nome, id_user, id_musica) VALUES (" + id + ", '" + nome + "' , " + LoginInfo.id + ", " + id_musica + ")";
				MySqlCommand cmd = new MySqlCommand(sql, _conn);
				cmd.ExecuteNonQuery();
			}
			else
			{
				string sql = "INSERT INTO playlists (id_playlist, nome, id_user, id_musica) VALUES (" + GetPlaylistsCount() + 1 + ", '" + nome + "' , " + LoginInfo.id + ", " + id_musica + ")";
				MySqlCommand cmd = new MySqlCommand(sql, _conn);
				cmd.ExecuteNonQuery();
			}
			
		}

		public void InserirArtistaOuvido(int id_artista, string nome)
		{
			string sql = "SELECT id_artista, ouvido FROM artistas_ouvidos";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			while (rdr.Read())
			{
				int cod = int.Parse(rdr[0].ToString());
				int ouvido = int.Parse(rdr[1].ToString());

				if(id_artista == cod)
				{
					rdr.Close();

					int o = ouvido + 1;

					sql = "UPDATE artistas_ouvidos SET ouvido = " + o + " WHERE id_artista = " + id_artista;
					cmd = new MySqlCommand(sql, _conn);
					cmd.ExecuteNonQuery();

					return;
				}
			}

			rdr.Close();

			sql = "INSERT INTO artistas_ouvidos (id_artista, nome, id_user, ouvido) VALUES (" + id_artista + ", '" + nome + "' , " + LoginInfo.id + ", " + 0 + ")";
			cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void DeletePlaylist(int id_playlist)
		{
			string sql = "DELETE FROM playlists WHERE id_playlist = " + id_playlist + " AND id_user = " + LoginInfo.id;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void DeleteMusicaPlaylist(int id_playlist, int id_musica)
		{
			string sql = "DELETE FROM playlists WHERE id_playlist = " + id_playlist + " AND id_user = " + LoginInfo.id + " AND id_musica = " + id_musica;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public int GetPlaylistsCount()
		{
			string sql = "SELECT COUNT(id_playlist) FROM playlists";
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

		public List<Artista> GetArtistasOuvidos(int id_user)
		{
			List<Artista> artistas = new List<Artista>();

			string sql = "SELECT id_artista, nome FROM artistas_ouvidos WHERE id_user = " + id_user + " ORDER BY ouvido DESC";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
				Artista artista = new Artista();
				artista.id = int.Parse(rdr[0].ToString());
				artista.Nome = rdr[1].ToString();
				artistas.Add(artista);
			}
			rdr.Close();

			if(artistas.Count < 6)
			{
				sql = "SELECT id_artista, nome FROM artistas_ouvidos ORDER BY ouvido DESC";
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					Artista artista = new Artista();
					artista.id = int.Parse(rdr[0].ToString());
					artista.Nome = rdr[1].ToString();

					bool add = true;

					for(int i = 0; i < artistas.Count; i++)
					{
						if (artistas[i].id == artista.id)
							add = false;
					}

					if(add)
						artistas.Add(artista);
				}
				rdr.Close();
			}

			for(int i = 0; i < artistas.Count; i++)
			{
				sql = "SELECT img FROM artistas WHERE id_artista = " + artistas[i].id;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				if (rdr.Read())
				{
					Artista artista = new Artista();
					artista.id = artistas[i].id;
					artista.Nome = artistas[i].Nome;
					artista.Img = rdr[0].ToString();
					artistas[i] = artista;
				}
				rdr.Close();
			}
			

			return artistas;
		}

		public List<PlaylistFavoritas> GetPlaylistsNomes()
		{
			List<PlaylistFavoritas> playlists = new List<PlaylistFavoritas>();
			string sql = "SELECT nome, id_user FROM playlists WHERE id_musica = -1";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			int id = -1;

			while (rdr.Read())
			{
				PlaylistFavoritas playlist = new PlaylistFavoritas();
				playlist.playlist = rdr[0].ToString();
				playlist.id_user = int.Parse(rdr[1].ToString());
				playlists.Add(playlist);
			}

			rdr.Close();

			//Obter o nome do utilizador
			for (int i = 0; i < playlists.Count; i++)
			{
				sql = "SELECT username FROM users WHERE id_user = " + playlists[i].id_user;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					playlists[i].user = rdr[0].ToString();
				}
				rdr.Close();
			}

			return playlists;
		}

		public int GetPlaylistByNome(string nome)
		{
			string sql = "SELECT id_playlist FROM playlists WHERE id_user = " + LoginInfo.id + " AND nome = '" + nome + "' ";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			int id = -1;

			if (rdr.Read())
			{
				id = int.Parse(rdr[0].ToString());
			}

			rdr.Close();

			return id;
		}


		public List<Musica> GetPlaylistsMusicas(int id)
		{
			string sql = "SELECT id_musica FROM playlists WHERE id_playlist = " + id;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			List<Musica> musicas = new List<Musica>();
			List<int> ids = new List<int>();

			while (rdr.Read())
			{
				if(int.Parse(rdr[0].ToString()) != -1)
			 		ids.Add(int.Parse(rdr[0].ToString()));
			}

			rdr.Close();

			for(int i = 0; i < ids.Count; i++)
			{
				Musica musica = Global.sql.ProcurarMusica(ids[i]);
				musicas.Add(musica);
			}

			return musicas;
		}

		//Retorna Artista procurando pelo nome
		public List<Artista> ProcurarArtistas(string nome, int qtd)
        {
			//TODO: Mudar o nome das vars
			List<Artista> artistas = new List<Artista>();
			string sql = "SELECT nome, id_artista, img FROM artistas WHERE nome LIKE '%" + nome + "%'";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";
			//int artId = -1;

			while (rdr.Read())
			{
				if (art != rdr[0].ToString() /*|| artId != int.Parse(rdr[1].ToString())*/)
				{
					Artista artista = new Artista();
					//var m = rdr[1].ToString();
					art = rdr[0].ToString();
					//artId = int.Parse(rdr[1].ToString());
					artista.Nome = art;
					artista.id = int.Parse(rdr[1].ToString());
					artista.Img = rdr[2].ToString();
					artistas.Add(artista);
				}
				else
				{
					Artista artista = new Artista();
					artista.Nome = "";
					artista.id = -1;
					artista.Img = "";
					artistas.Add(artista);
				}
			}

			rdr.Close();


			if (artistas.Count > 0)
			{
				return artistas;
			}

			return new List<Artista>();
		}

        //Retorna Artista procurando pelo codigo
        public Artista ProcurarArtista(int cod)
        {
			string sql = "SELECT nome, img FROM artistas WHERE id_artista = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				Artista artista = new Artista();
				artista.id = cod;
				artista.Nome = rdr[0].ToString();
				artista.Img = rdr[1].ToString();
				rdr.Close();
				return artista;
			}

			rdr.Close();

			return new Artista();
		}

		public void AlterarArtista(int cod, string nome, string img)
		{
			if(img == "")
			{
				Artista artista = ProcurarArtista(cod);
				img = artista.Img;
			}

			string sql = "UPDATE artistas SET nome = '" + nome + "' , img = '" + img + "' WHERE id_artista = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void AlterarMusica(int cod, string nome, int id_artista)
		{
			string sql = "UPDATE musicas SET nome = '" + nome + "' , id_artista = " + id_artista + " WHERE id_musica = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void AlterarAlbum(int cod, string nome)
		{
			string sql = "UPDATE albums SET nome = '" + nome + "' WHERE id_album = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void AlterarUtilizador(int cod, string username, string password, string nome)
		{
			string sql = "UPDATE users SET username = '" + username + "', password = '" + password + "', nome = '" + nome + "' WHERE id_user = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		//Retorna Album procurando pelo codigo
		public Album ProcurarAlbum(int cod)
		{
			Album album = new Album();
			string sql = "SELECT nome FROM albums WHERE id_album = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			if(rdr.Read())
			{
				if (art != rdr[0].ToString())
				{
					art = rdr[0].ToString();
					album.id = cod;
					album.Nome = rdr[0].ToString();
				}
			}
			rdr.Close();

			return album;
		}

		//Retorna o album com os ids das musicas
		public Album ProcurarAlbumMusicas(int cod)
		{
			Album album = new Album();
			string sql = "SELECT nome, id_musica FROM albums WHERE id_album = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			int art = 0;

			album.Musicas = new List<Musica>();

			while (rdr.Read())
			{
				if (art != int.Parse(rdr[1].ToString()))
				{
					art = int.Parse(rdr[1].ToString());
					Musica musica = new Musica();
					musica.id = int.Parse(rdr[1].ToString());
					album.id = cod;
					album.Nome = rdr[0].ToString();
					album.Musicas.Add(musica);
				}
			}
			rdr.Close();

			for(int i = 0; i < album.Musicas.Count; i++)
			{
				album.Musicas[i] = ProcurarMusica(album.Musicas[i].id);
			}

			return album;
		}
		//Retorna Musica procurando pelo codigo
		public Musica ProcurarMusica(int cod)
		{
			List<Artista> artista = new List<Artista>();
			artista = GetTodosArtistas();

			string sql = "SELECT nome, id_artista FROM musicas WHERE id_musica = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				var m = rdr[1].ToString();
				Musica musica = new Musica();
				musica.id = cod;
				musica.Nome = rdr[0].ToString();
				musica.artista = artista[int.Parse(m) - 1];
				rdr.Close();
				return musica;
			}

			rdr.Close();

			return new Musica();
		}

		public List<Musica> ProcurarMusicasPorArtista(int cod, int qtd)
        {
            Musica[] musica = new Musica[qtd];
            List<Artista> artista = new List<Artista>();
            artista = GetTodosArtistas();
            string sql = "SELECT nome, id_artista, id_musica FROM musicas WHERE id_artista = " + cod;
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
						musica[i].id = int.Parse(rdr[2].ToString());
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
            List<Artista> artista = new List<Artista>();
            artista = GetTodosArtistas();

			List<Musica> musicas = new List<Musica>();

            string sql = "SELECT nome, id_artista, id_musica FROM musicas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int artId = -1;
            while (rdr.Read())
            {
                if (art != rdr[0].ToString() || artId != int.Parse(rdr[1].ToString()))
                {
                    art = rdr[0].ToString();
                    artId = int.Parse(rdr[1].ToString());
					Musica musica = new Musica();
                    musica.Nome = art;
					for(int i = 0; i < artista.Count; i++)
					{
						if(artista[i].id == (artId - 1))
							musica.artista = artista[artId - 1];
					}
	                musica.id = int.Parse(rdr[2].ToString());
					musicas.Add(musica);
                }
                else
                {
					Musica musica = new Musica();
					musica.Nome = "";
                    musica.artista = new Artista();
	                musica.id = -1;
                }
            }
            
			
            rdr.Close();

	        if (musicas.Count > 0)
	        {
		        return musicas;
			}

	        return new List<Musica>();
        }

        public List<Album> ProcurarAlbums(string nome, int qtd)
        {
            Album[] album = new Album[qtd];
            string sql = "SELECT nome, id_album, id_musica FROM albums WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            string art = "";
            int artId = -1;
			Musica musica = new Musica();

            for (int i = 0; i < qtd; i++)
            {
                if (rdr.Read())
                {
                    if (art != rdr[0].ToString())
                    {
                        art = rdr[0].ToString();
                        album[i].Nome = art;
	                    album[i].id = int.Parse(rdr[1].ToString());
						musica.id = int.Parse(rdr[2].ToString());
						musica.Nome = "";
						album[i].Musicas = new List<Musica>();
						album[i].Musicas.Add(musica);
					}
                    else
                    {
                        album[i].Nome = "";
	                    album[i].id = -1;
					}
                }
                else
                {
                    album[i].Nome = "";
	                album[i].id = -1;
				}
            }

            rdr.Close();

            List<Album> albums = new List<Album>();
            for (int i = 0; i < album.Length; i++)
            {
                if (album[i].Nome != "")
                {
					musica = ProcurarMusica(album[i].Musicas[0].id);
					album[i].Musicas[0] = musica;
					albums.Add(album[i]);
                }
            }

            if (albums.Count > 0)
            {
                return albums;
            }

			return new List<Album>();
		}

        public int GetCodigoArtista(string nome)
        {
			//TODO: Verificar se existe na base dados (fazer mesma coisa que no ProcurarMusicas)
            if (nome.Contains("'"))
                nome = nome.Replace("'", " ");
            string sql = "SELECT id_artista FROM artistas WHERE nome = '" + nome + "'";
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
            string sql = "SELECT nome, img, id_artista FROM artistas";
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
					artista.id = int.Parse(rdr[2].ToString());
                    artistas.Add(artista);
                }
                else
                {
                    Artista artista = new Artista();
                    artista.Nome = rdr[0].ToString();
                    artista.Img = rdr[1].ToString();
					artista.id = int.Parse(rdr[2].ToString());
					artistas.Add(artista);
                }

                i++;
            }
            rdr.Close();

            return artistas;
        }

		public List<Artista> GetTodosArtistasOrdered()
		{
			List<Artista> artistas = new List<Artista>();
			string sql = "SELECT nome, img, id_artista FROM artistas ORDER BY nome";
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
					artista.id = int.Parse(rdr[2].ToString());
					artistas.Add(artista);
				}
				else
				{
					Artista artista = new Artista();
					artista.Nome = rdr[0].ToString();
					artista.Img = rdr[1].ToString();
					artista.id = int.Parse(rdr[2].ToString());
					artistas.Add(artista);
				}

				i++;
			}
			rdr.Close();

			return artistas;
		}

		public List<Album> GetTodosAlbums()
		{
			List<Album> albums = new List<Album>();
			string sql = "SELECT nome, id_album FROM albums GROUP BY nome";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";
			int i = 0;
			while (rdr.Read())
			{
				if (!(art == rdr[0]))
				{
					art = rdr[0].ToString();
					Album artista = new Album();
					artista.Nome = rdr[0].ToString();
					artista.id = int.Parse(rdr[1].ToString());
					albums.Add(artista);
				}
				else
				{
					Album artista = new Album();
					artista.Nome = rdr[0].ToString();
					artista.id = int.Parse(rdr[1].ToString());
					albums.Add(artista);
				}

				i++;
			}
			rdr.Close();

			return albums;
		}

		public List<Musica> GetTodasMusicas()
        {
            List<Musica> musicas = new List<Musica>();
            string sql = "SELECT id_musica, nome FROM musicas ORDER BY nome";
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

		public List<Musica> GetTodasMusicasArtista(int id_artista)
		{
			Artista artista = ProcurarArtista(id_artista);

			List<Musica> musicas = new List<Musica>();
			string sql = "SELECT id_musica, nome FROM musicas WHERE id_artista = " + id_artista;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";
			int i = 0;
			while (rdr.Read())
			{
				if (!(art == rdr[0]))
				{
					art = rdr[0].ToString();
					Musica musica = new Musica { id = int.Parse(rdr[0].ToString()), Nome = rdr[1].ToString() };
					musica.artista = artista;
					musicas.Add(musica);
				}
				else
				{
					Musica musica = new Musica { Nome = rdr[0].ToString() };
					musica.artista = artista;
					musicas.Add(musica);
				}

				i++;
			}
			rdr.Close();

			return musicas;
		}

		//Retorna todos os utilizadoes
		public List<User> GetTodosUsers()
		{
			List<User> users = new List<User>();
			string sql = "SELECT id_user, username, password FROM users";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				User user = new User();
				user.id = int.Parse(rdr[0].ToString());
				user.username = rdr[1].ToString();
				user.password = rdr[2].ToString();
				users.Add(user);
			}
			rdr.Close();

			return users;

		}

		//Retorna todos os artistas favoritos de todos os utilizadores
		public List<ArtistasFavoritos> GetArtistasFavoritosAdmin()
		{
			List<ArtistasFavoritos> artistas = new List<ArtistasFavoritos>();
			string sql = "SELECT id_artista, id_user FROM artistas_favoritos";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				ArtistasFavoritos artista = new ArtistasFavoritos();
				artista.id_artista = int.Parse(rdr[0].ToString());
				artista.id_user = int.Parse(rdr[1].ToString());
				artistas.Add(artista);
			}
			rdr.Close();

			//Obter o nome do utilizador

			for(int i = 0; i < artistas.Count; i++)
			{
				sql = "SELECT nome FROM artistas WHERE id_artista = " + artistas[i].id_artista;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					artistas[i].artista = rdr[0].ToString();
				}
				rdr.Close();
			}
			
			//Obter o nome do artista

			for(int i = 0;i < artistas.Count; i++)
			{
				sql = "SELECT username FROM users WHERE id_user = " + artistas[i].id_user;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();
				art = "";

				while (rdr.Read())
				{

					artistas[i].user = rdr[0].ToString();
				}

				rdr.Close();
			}

			return artistas;

		}

		//Retorna todos os albums favoritos de todos os utilizadores
		public List<AlbumsFavoritos> GetAlbumsFavoritosAdmin()
		{
			List<AlbumsFavoritos> albums = new List<AlbumsFavoritos>();
			string sql = "SELECT id_album, id_user FROM albums_favoritos";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				AlbumsFavoritos artista = new AlbumsFavoritos();
				artista.id_album = int.Parse(rdr[0].ToString());
				artista.id_user = int.Parse(rdr[1].ToString());
				albums.Add(artista);
			}
			rdr.Close();

			//Obter o nome do utilizador
			for (int i = 0; i < albums.Count; i++)
			{
				sql = "SELECT nome FROM albums WHERE id_album = " + albums[i].id_album;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					albums[i].album = rdr[0].ToString();
				}
				rdr.Close();
			}

			//Obter o nome do artista

			for (int i = 0; i < albums.Count; i++)
			{
				sql = "SELECT username FROM users WHERE id_user = " + albums[i].id_user;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();
				art = "";

				while (rdr.Read())
				{

					albums[i].user = rdr[0].ToString();
				}

				rdr.Close();
			}

			return albums;

		}

		//Retorna todos as musicas favoritas de todos os utilizadores
		public List<MusicasFavoritas> GetMusicasFavoritasAdmin()
		{
			List<MusicasFavoritas> musicas = new List<MusicasFavoritas>();
			string sql = "SELECT id_musica, id_user FROM musicas_favoritas";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				MusicasFavoritas artista = new MusicasFavoritas();
				artista.id_musica = int.Parse(rdr[0].ToString());
				artista.id_user = int.Parse(rdr[1].ToString());
				musicas.Add(artista);
			}
			rdr.Close();

			//Obter o nome do utilizador
			for (int i = 0; i < musicas.Count; i++)
			{
				sql = "SELECT nome FROM musicas WHERE id_musica = " + musicas[i].id_musica;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					musicas[i].musica = rdr[0].ToString();
				}
				rdr.Close();
			}

			//Obter o nome do artista

			for (int i = 0; i < musicas.Count; i++)
			{
				sql = "SELECT username FROM users WHERE id_user = " + musicas[i].id_user;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();
				art = "";

				while (rdr.Read())
				{

					musicas[i].user = rdr[0].ToString();
				}

				rdr.Close();
			}

			return musicas;

		}

		public List<Artista> GetArtistasFavoritos(int cod)
		{
			List<Artista> artistas = new List<Artista>();
			string sql = "SELECT id_artista FROM artistas_favoritos WHERE id_user = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				string id = rdr[0].ToString();
				if (art != id)
				{
					art = rdr[0].ToString();
					Artista artista = new Artista { id = int.Parse(rdr[0].ToString()) };
					artistas.Add(artista);
				}
				
			}
			rdr.Close();

			return artistas;   
		}

		public List<Album> GetAlbumsFavoritos(int cod)
		{
			List<Album> albums = new List<Album>();
			string sql = "SELECT id_album FROM albums_favoritos WHERE id_user = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				string id = rdr[0].ToString();
				if (art != id)
				{
					art = rdr[0].ToString();
					Album album = new Album { id = int.Parse(rdr[0].ToString()) };
					albums.Add(album);
				}

			}
			rdr.Close();

			return albums;
		}

		public void SubstituirMusicaAlbum(int Antcod, int newcod, int codAlbum)
		{
			string sql = "UPDATE albums SET id_musica = " + newcod + " WHERE id_album = " + codAlbum + " AND id_musica = " + Antcod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void AdicionarMusicaAlbum(int cod, int codAlbum)
		{
			string sql = "INSERT INTO albums (id_album, id_musica) VALUES (" + codAlbum + ", " + cod + ")";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public List<Musica> GetMusicasFavoritas(int cod)
		{
			List<Musica> musicas = new List<Musica>();
			string sql = "SELECT id_musica FROM musicas_favoritas WHERE id_user = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";

			while (rdr.Read())
			{
				string id = rdr[0].ToString();
				if (art != id)
				{
					art = rdr[0].ToString();
					Musica musica = new Musica();
					musica.id = int.Parse(rdr[0].ToString());
					musicas.Add(musica);
				}

			}
			rdr.Close();

			return musicas;
		}

		public List<Playlist> GetTodasPlaylists(int cod)
		{
			List<Playlist> playlists = new List<Playlist>();
			string sql = "SELECT id_playlist, nome FROM playlists WHERE id_user = " + cod + " AND id_musica = -1";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			int art = -1;

			while (rdr.Read())
			{
				int id = int.Parse(rdr[0].ToString());
				if (art != id)
				{
					art = int.Parse(rdr[0].ToString());
					Playlist playlist = new Playlist();
					playlist.id = id;
					playlist.nome = rdr[1].ToString();
					playlist.id_user = cod;

					playlists.Add(playlist);
				}

			}
			rdr.Close();

			return playlists;
		}

		public void DeleteArtists(int cod)
		{
			string sql = "DELETE FROM artistas WHERE id_artista = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void DeleteAlbum(int cod)
		{
			string sql = "DELETE FROM albums WHERE id_album = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void DeleteMusica(int cod)
		{
			string sql = "DELETE FROM musicas WHERE id_musica = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}
		
		public void DeleteUtilizador(int cod)
		{
			string sql = "DELETE FROM users WHERE id_user = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public void DeleteMusicaAlbum(int cod_album, int cod_musica)
		{
			string sql = "DELETE FROM albums WHERE id_musica = " + cod_musica + " AND id_album = " + cod_album;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			cmd.ExecuteNonQuery();
		}

		public User GetUtilizador(int cod)
		{
			User user = new User();
			string sql = "SELECT username, password, nome FROM users WHERE id_user = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			if(rdr.Read())
			{
				user.id = cod;
				user.username = rdr[0].ToString();
				user.password = rdr[1].ToString();
				user.nome = rdr[2].ToString();
			}
			rdr.Close();

			return user;
		}

		public List<Playlist> GetPlaylistsUser(int id)
		{
			List<Playlist> playlists = new List<Playlist>();

			string sql = "SELECT id_playlist, nome FROM playlists WHERE id_user = " + id + " AND id_musica = -1";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
				Playlist playlist = new Playlist();
				playlist.id = int.Parse(rdr[0].ToString());
				playlist.nome = rdr[1].ToString();
				playlists.Add(playlist);
			}
			rdr.Close();

			return playlists;
		}

		public List<User> GetUsersFromArtistasFav(int id)
		{
			List<User> users = new List<User>();

			string sql = "SELECT id_user FROM artistas_favoritos WHERE id_artista = " + id;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();

			while (rdr.Read())
			{
				User user = new User();
				user.id = int.Parse(rdr[0].ToString());
				users.Add(user);
			}
			rdr.Close();


			for(int i = 0; i < users.Count; i++)
			{
				sql = "SELECT email FROM users WHERE id_user = " + users[i].id;
				cmd = new MySqlCommand(sql, _conn);
				rdr = cmd.ExecuteReader();

				if (rdr.Read())
				{
					users[i].email = rdr[0].ToString();
				}
				rdr.Close();
			}

			return users;
		}

		public bool RegistarUtilizador(string username, string password, string nome, string email)
        {
            if (username.Contains("'"))
            {
                MessageBox.Show("Nao pode ter \' no username");
                return false;
            }

			if(!email.Contains("@"))
			{
				MessageBox.Show("O email está incorreto");
				return false;
			}

			if(username == "")
			{
				MessageBox.Show("Tem de preencher o username");
				return false;
			}
			else if(password == "")
			{
				MessageBox.Show("Tem de preencher a password");
				return false;
			}
			else if(nome == "")
			{
				MessageBox.Show("Tem de preencher o nome");
				return false;
			}
			else if(email == "")
			{
				MessageBox.Show("Tem de preencher o email");
				return false;
			}

            string sql = "INSERT INTO users (username, password, nome, email) VALUES ('" + username + "', '" + password + "', '" + nome + "' , '" + email + "')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();

			return true;
        }

        public bool LoginUtilizador(string username, string password)
        {
            string sql = "SELECT id_user, username, password FROM users";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr[1].ToString() == username)
                {
                    if (rdr[2].ToString() == password)
                    {
	                    LoginInfo.username = username;
	                    LoginInfo.id = int.Parse(rdr[0].ToString());
						rdr.Close();
						return true;
                    }
                }
            }
            rdr.Close();

			MessageBox.Show("Credenciais incorretas");

			return false;
        }
    }
}