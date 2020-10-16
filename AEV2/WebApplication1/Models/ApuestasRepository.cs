using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace WebApplication1.Models
{
    public class ApuestasRepository
    {
        private MySqlConnection Conexion()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=placemybet;SslMode=none";
            MySqlConnection conexion = new MySqlConnection(connectionString);
            return conexion;
        }

        internal List<Apuesta> Retrieve()
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM APUESTA";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<Apuesta> apuestas = new List<Apuesta>();

                while (reader.Read())
                {
                    Apuesta ap = new Apuesta(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetDateTime(4));
                    apuestas.Add(ap);
                }
                connection.Close();
                return apuestas;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error al conectarse con la Base de Datos.");
                return null;
            }
        }

        internal void Save(Apuesta apuesta)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO apuestas(idApuesta,tipo,cuota,dinero,fechaApuesta,USUARIO_email) VALUES ('" + apuesta.IdApuesta + "' , '" + apuesta.Tipo + "' ,'" + apuesta.Cuota + "' ,'" + apuesta.Dinero + "' ,'" + apuesta.FechaApuesta + "' , '" + apuesta.FechaApuesta + "' );";

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Insert Apuesta");
            }
        }
    }
}