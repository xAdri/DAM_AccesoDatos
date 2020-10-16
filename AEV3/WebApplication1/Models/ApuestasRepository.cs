using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Globalization;

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

        internal List<ApuestaDTO> RetrieveDTO()
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM APUESTA";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<ApuestaDTO> apuestas = new List<ApuestaDTO>();

                while (reader.Read())
                {
                    ApuestaDTO ap = new ApuestaDTO(reader.GetString(0), reader.GetDouble(1), reader.GetDouble(2), reader.GetDateTime(3));
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

            // . = ,
            CultureInfo cultInfo = new System.Globalization.CultureInfo("es-ES");
            cultInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cultInfo.NumberFormat.PercentDecimalSeparator = ".";
            cultInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cultInfo;

            command.CommandText = "INSERT INTO apuestas(idApuesta,tipo,cuota,dinero,fechaApuesta,USUARIO_email) VALUES ('" + apuesta.IdApuesta + "' , '" + apuesta.Tipo + "' ,'" + apuesta.Cuota + "' ,'" + apuesta.Dinero + "' ,'" + apuesta.FechaApuesta + "' , '" + apuesta.FechaApuesta + "' );";

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                string tipoApuesta = apuesta.Tipo.ToLower();

                // No me da tiempo a acabarlo para la entrega de viernes, 16 de octubre de 2020, 23:59, en la AEV4 haré commit de lo que falta, lo siento
                if (tipoApuesta == "over")
                {

                }
                else if (tipoApuesta == "under")
                {

                }


                // No me da tiempo a acabar el trabajo a tiempo para antes de la hora de entrega, estará para el pr
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Insert Apuesta");
            }
        }
    }
}