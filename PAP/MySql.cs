﻿using System;
using MySql.Data;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PAP
{
    public class Sql
    {
        private MySql.Data.MySqlClient.MySqlConnection _conn;
        private string _myConnectionString;

        public Sql()
        {


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

        public void InserirArtistas(string nome, string img = "")
        {
            string sql = "INSERT INTO artistas (nome, img) VALUES ('" + nome + "', '" + img + "')";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public Artista ProcurarArtistas(string nome, int qtd)
        {
            //TODO: Mudar isto para Spotify
            Artista artista = new Artista();
            string sql = "SELECT nome, img FROM artistas WHERE nome LIKE '%" + nome + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, _conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            for (int i = 0; i < qtd; i++)
            {
                rdr.Read();
                Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                //TODO: Atribuir isto para artista e dar return
            }
            rdr.Close();

            return artista;
        }
    }
}