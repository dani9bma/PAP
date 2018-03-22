using System;
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

	public class ArtistasFavoritos
	{
		public int id_user;
		public int id_artista;
		public string user;
		public string artista;

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
			//TODO: Verificar se artista ja foi adicionado aos favoritos
			string sql = "INSERT INTO artistas_favoritos (id_artista, id_user) VALUES (" + id_artista + ", " + id_user + ")";
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
				string sql = "INSERT INTO playlists (nome, id_user, id_musica) VALUES ('" + nome + "' , " + LoginInfo.id + ", " + id_musica + ")";
				MySqlCommand cmd = new MySqlCommand(sql, _conn);
				cmd.ExecuteNonQuery();
			}
			
		}

		public int GetPlaylistByNome(string nome)
		{
			string sql = "SELECT id_playlist FROM playlists WHERE id_user = " + LoginInfo.id + " AND nome LIKE '" + nome + "' ";
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

        //Retorna Artista procurando pelo nome
        public List<Artista> ProcurarArtistas(string nome, int qtd)
        {
			//TODO: Mudar o nome das vars
			Artista[] musica = new Artista[qtd];
			string sql = "SELECT nome, id_artista, img FROM artistas WHERE nome LIKE '%" + nome + "%'";
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			string art = "";
			//int artId = -1;

			for (int i = 0; i < qtd; i++)
			{
				if (rdr.Read())
				{
					if (art != rdr[0].ToString() /*|| artId != int.Parse(rdr[1].ToString())*/)
					{
						//var m = rdr[1].ToString();
						art = rdr[0].ToString();
						//artId = int.Parse(rdr[1].ToString());
						musica[i].Nome = art;
						musica[i].id = int.Parse(rdr[1].ToString());
						musica[i].Img = rdr[2].ToString();
					}
					else
					{
						musica[i].Nome = "";
						musica[i].id = -1;
						musica[i].Img = "";
					}
				}
				else
				{
					musica[i].Nome = "";
					musica[i].id = -1;
					musica[i].Img = "";
				}
			}

			rdr.Close();

			List<Artista> mu = new List<Artista>();
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

			MessageBox.Show("O artista que tentou pesquisar não existe na nossa base de dados, por favor entre aqui (LINK) e contribua com a música que deseja");
			//TODO: Quando nao encontra musica abrir outra janela com um formulario com o nome da musica, link da musica, artista(<select>) e um botao de "Enviar", clicado no botao este deverá fazer o download da musica e adicionala na base de dados
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

			return album;
		}
		//Retorna Musica procurando pelo codigo
		public Musica ProcurarMusica(int cod)
		{
			string sql = "SELECT nome, id_artista FROM musicas WHERE id_musica = " + cod;
			MySqlCommand cmd = new MySqlCommand(sql, _conn);
			MySqlDataReader rdr = cmd.ExecuteReader();
			if (rdr.Read())
			{
				Musica musica = new Musica();
				musica.id = cod;
				musica.Nome = rdr[0].ToString();
				musica.artista.id = int.Parse(rdr[1].ToString());
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
            List<Artista> artista = new List<Artista>();
            artista = GetTodosArtistas();

			List<Musica> musicas = new List<Musica>();

            string sql = "SELECT nome, id_artista, id_musica FROM musicas WHERE nome LIKE '%" + nome + "%'";
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
						Musica musica = new Musica();
                        musica.Nome = art;
                        musica.artista = artista[int.Parse(m) - 1];
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

	        MessageBox.Show("A musica que tentou pesquisar não existe na nossa base de dados, por favor entre aqui (LINK) e contribua com a música que deseja");
            //TODO: Quando nao encontra musica abrir outra janela com um formulario com o nome da musica, link da musica, artista(<select>) e um botao de "Enviar", clicado no botao este deverá fazer o download da musica e adicionala na base de dados
	        return new List<Musica>();
        }

        public List<Album> ProcurarAlbums(string nome, int qtd)
        {
            Album[] album = new Album[qtd];
            string sql = "SELECT nome, id_album FROM albums WHERE nome LIKE '%" + nome + "%'";
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
	                    album[i].id = int.Parse(rdr[1].ToString());
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
                    albums.Add(album[i]);
                }
            }

            if (albums.Count > 0)
            {
                return albums;
            }

            MessageBox.Show("O album que tentou pesquisar não existe na nossa base de dados, por favor entre aqui (LINK) e contribua com a música que deseja");
			//TODO: Quando nao encontra musica abrir outra janela com um formulario com o nome da musica, link da musica, artista(<select>) e um botao de "Enviar", clicado no botao este deverá fazer o download da musica e adicionala na base de dados
			return new List<Album>();
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
			string sql = "SELECT id_playlist, nome FROM playlists WHERE id_user = " + cod;
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

		public bool RegistarUtilizador(string username, string password)
        {
            if (username.Contains("'"))
            {
                MessageBox.Show("Nao pode ter \' no username");
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

            string sql = "INSERT INTO users (username, password) VALUES ('" + username + "', '" + password + "')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();

			return true;
        }

        public bool LoginUtilizador(string username, string password)
        {
            string sql = "SELECT id_user, username, password FROM users";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
			bool encontrou = false;

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