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

        internal void RetrieveApuestasEmail(string email, double tipoMercado)
        {
            // Esto esta mal
            string query = "SELECT * FROM apuesta " + "INNER JOIN mercado ON apuesta.USUARIO_email = mercado.idMercado " + "INNER JOIN evento ON evento.idEvento = mercado.USUARIO_email " + "WHERE email = @email AND overUnder = @overUnder;";

            MySqlCommand command = new MySqlCommand(query);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@overUnder", tipoMercado);
        }
    }
}