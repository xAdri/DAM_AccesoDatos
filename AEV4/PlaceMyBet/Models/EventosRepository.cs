using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace WebApplication1.Models
{
    public class EventosRepository
    {
        private MySqlConnection Conexion()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=placemybet;SslMode=none";
            MySqlConnection conexion = new MySqlConnection(connectionString);
            return conexion;
        }

        internal List<Evento> Retrieve()
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM EVENTO";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<Evento> eventos = new List<Evento>();

                while (reader.Read())
                {
                    Evento ev = new Evento(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                    eventos.Add(ev);
                }
                connection.Close();
                return eventos;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error al conectarse con la Base de Datos.");
                return null;
            }
        }

        internal List<EventoDTO> RetrieveDTO()
        {
            List<EventoDTO> eventos = new List<EventoDTO>();
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM EVENTO";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string equipoLocal = reader.GetString(1);
                    string equipoVisitante = reader.GetString(2);
                    DateTime fecha = reader.GetDateTime(3);
                    eventos.Add(new EventoDTO(equipoLocal, equipoVisitante, fecha));
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error connection");
            }
            return eventos;
        }

        internal List<Evento> RetriveEventoTipoMercado(string tipo, int idEvento)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand comand = connection.CreateCommand();

            //comand.CommandText = "select * from evento INNER JOIN mercado ON evento.idEvento where 'overUnder' =" + tipo;
            comand.CommandText = "SELECT * FROM `mercado` WHERE `overUnder` LIKE '" + tipo + "' AND `EVENTO_idEvento` = '" + idEvento + "'";

            connection.Open();

            MySqlDataReader reader = comand.ExecuteReader();
            List<Evento> eventos = new List<Evento>();


            while (reader.Read())
            {
                Evento ev = new Evento(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                eventos.Add(ev);
            }

            connection.Close();
            return eventos;
        }

        internal List<Mercado> RetriveTipoMercado(string tipo, int idEvento)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand comand = connection.CreateCommand();

            //comand.CommandText = "select * from evento INNER JOIN mercado ON evento.idEvento where 'overUnder' =" + tipo;
            comand.CommandText = "SELECT * FROM `mercado` WHERE `overUnder` LIKE '" + tipo + "' AND `EVENTO_idEvento` = '" + idEvento + "'";
            connection.Open();

            MySqlDataReader reader = comand.ExecuteReader();
            List<Mercado> mercados = new List<Mercado>();


            while (reader.Read())
            {
                Mercado ev = new Mercado(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));
                mercados.Add(ev);
            }

            connection.Close();
            return mercados;
        }
    }
}