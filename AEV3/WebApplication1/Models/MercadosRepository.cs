using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace WebApplication1.Models
{
    public class MercadosRepository
    {
        private MySqlConnection Conexion()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=placemybet;SslMode=none";
            MySqlConnection conexion = new MySqlConnection(connectionString);
            return conexion;
        }

        internal List<Mercado> Retrieve()
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM MERCADO";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<Mercado> mercados = new List<Mercado>();

                while (reader.Read())
                {
                    Mercado me = new Mercado(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));
                    mercados.Add(me);
                }
                connection.Close();
                return mercados;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error al conectarse con la Base de Datos.");
                return null;
            }
        }

        internal List<MercadoDTO> RetrieveDTO()
        {
            List<MercadoDTO> mercados = new List<MercadoDTO>();
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM MERCADO";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string overUnder = reader.GetString(1);
                    double cuotaOver = reader.GetDouble(2);
                    double cuotaUnder = reader.GetDouble(3);
                    double dineroOver = reader.GetDouble(4);
                    double dineroUnder = reader.GetDouble(5);

                    mercados.Add(new MercadoDTO(overUnder, cuotaOver, cuotaUnder, dineroOver, dineroUnder));
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error connection");
            }
            return mercados;
        }
    }
}