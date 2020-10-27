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

            //command.CommandText = "INSERT INTO apuestas(idApuesta,tipo,cuota,dinero,fechaApuesta,USUARIO_email) VALUES ('" + apuesta.IdApuesta + "' , '" + apuesta.Tipo + "' ,'" + apuesta.Cuota + "' ,'" + apuesta.Dinero + "' ,'" + apuesta.FechaApuesta + "' , '" + apuesta.FechaApuesta + "' );";
            command.CommandText = "Select * from mercado where idMercado " + apuesta.IdApuesta + ";";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                Mercado mercado = new Mercado(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));

                // Probabilidad dependiendo de Over Under
                double resultado = 0;
                double probabilidad = 0;
                string tipoApuesta = apuesta.Tipo.ToLower();

                if (tipoApuesta == "over")
                {
                    probabilidad = mercado.DineroOver / (mercado.DineroOver + mercado.DineroUnder);
                }
                else if (tipoApuesta == "under")
                {
                    probabilidad = mercado.DineroUnder / (mercado.DineroOver + mercado.DineroUnder);
                }

                resultado = 1 / probabilidad * 0.95;

                if (tipoApuesta == "over")
                {
                    command.CommandText = "Update mercado set CuotaOver =" + Math.Round(resultado, 2) + ", DineroOver =" + mercado.DineroOver + ";";
                }
                else if (tipoApuesta == "under")
                {
                    command.CommandText = "Update mercado set CuotaUnder =" + Math.Round(resultado, 2) + ", DineroUnder =" + mercado.DineroUnder + ";";
                }

                command.ExecuteNonQuery();
                reader.Close();
                connection.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine("Error de conexion Insert Apuesta");
            }
        }


    }
}