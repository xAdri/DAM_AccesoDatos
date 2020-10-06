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
    }
}