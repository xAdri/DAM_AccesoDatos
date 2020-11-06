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
                    Apuesta ap = new Apuesta(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetMySqlDateTime(5).ToString(), reader.GetString(6));
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
                    ApuestaDTO ap = new ApuestaDTO(reader.GetString(5), reader.GetString(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetMySqlDateTime(4).ToString());
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
            MySqlCommand commandDinero = connection.CreateCommand();
            MySqlCommand commandCuotas = connection.CreateCommand();

            // . = ,
            CultureInfo cultInfo = new System.Globalization.CultureInfo("es-ES");
            cultInfo.NumberFormat.NumberDecimalSeparator = ".";
            cultInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            cultInfo.NumberFormat.PercentDecimalSeparator = ".";
            cultInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = cultInfo;

            DateTime dt = DateTime.Now;
            string fechaApuesta = dt.ToString("yyyyMMddHHmmss");

            command.CommandText = "INSERT INTO apuesta (idApuesta,tipo,cuota,dinero,fechaApuesta,USUARIO_email) VALUES ('" + apuesta.IdApuesta + "' , '" + apuesta.Tipo + "' ,'" + apuesta.Cuota + "' ,'" + apuesta.Dinero + "' ,'" + fechaApuesta + "' , '" + apuesta.Email + "' );";
            commandDinero.CommandText = "";

            try
            {
                connection.Open();

                // Probabilidad dependiendo de Over Under
                string tipoApuesta = apuesta.Tipo.ToLower();
                double over = ResolverDineroOver(apuesta.IdMercado);
                double under = ResolverDineroUnder(apuesta.IdMercado);
                double probabilidadOver = ResolverProbabilidadOver(over, under);
                double probabilidadUnder = ResolverProbabilidadUnder(over, under);
                double cuota;

                if (tipoApuesta == "over")
                {
                    cuota = (double)ResolverCuotaOver(apuesta.IdMercado);
                    double calcularOver = ResolverCuota(probabilidadOver);

                }
                else if (tipoApuesta == "under")
                {
                    cuota = (double)ResolverCuotaUnder(apuesta.IdMercado);
                }
                
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e);
            }
        }

        internal double ResolverCuotaOver(int mercado)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT CuotaOver FROM mercados WHERE IdMercado = " + mercado + ";";

            connection.Open();
            double cuota = Convert.ToDouble(command.ExecuteScalar());
            connection.Close();

            return cuota;
        }

        internal double ResolverCuotaUnder(int mercado)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT CuotaUnder FROM mercados WHERE IdMercado = " + mercado + ";";

            connection.Open();
            double cuota = Convert.ToDouble(command.ExecuteScalar());
            connection.Close();

            return cuota;
        }

        internal double ResolverDineroOver(int mercado)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT DineroOver FROM mercados WHERE IdMercado =" + mercado + ";";

            connection.Open();
            double probabilidad = Convert.ToDouble(command.ExecuteScalar());
            connection.Close();

            return probabilidad;
        }

        internal double ResolverDineroUnder(int mercado)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT DineroUnder FROM mercados WHERE IdMercado =" + mercado + ";";

            connection.Open();
            double probabilidad = Convert.ToDouble(command.ExecuteScalar());
            connection.Close();

            return probabilidad;
        }

        internal double ResolverCuota(double probabilidad)
        {
            double cuota = (1 / probabilidad) * 0.95;
            return cuota;
        }

        internal double ResolverProbabilidadUnder(double dineroOver, double dineroUnder)
        {
            double probabilidad = dineroUnder / (dineroOver + dineroUnder);
            return probabilidad;
        }

        internal double ResolverProbabilidadOver(double dineroOver, double dineroUnder)
        {
            double probabilidad = dineroOver / (dineroOver + dineroUnder);
            return probabilidad;
        }

    }
}