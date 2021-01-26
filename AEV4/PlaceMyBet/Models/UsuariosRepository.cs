using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace WebApplication1.Models
{
    public class UsuariosRepository
    {
        private MySqlConnection Conexion()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=placemybet;SslMode=none";
            MySqlConnection conexion = new MySqlConnection(connectionString);
            return conexion;
        }

        internal List<Usuario> Retrieve()
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM USUARIO";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<Usuario> eventos = new List<Usuario>();

                while (reader.Read())
                {
                    Usuario us = new Usuario(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    eventos.Add(us);
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

        internal List<Apuesta> RetrieveApuestasEmail(string email, string tipo)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM `apuesta` WHERE `tipo` LIKE '" + tipo + "' AND `USUARIO_email` LIKE '" + email + "'";
            connection.Open();


            MySqlDataReader reader = command.ExecuteReader();
            List<Apuesta> apuestas = new List<Apuesta>();

            while (reader.Read())
            {
                Apuesta ev = new Apuesta(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetString(5), reader.GetString(6));
                apuestas.Add(ev);
            }

            connection.Close();
            return apuestas;
        }
    }
}