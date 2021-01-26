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
                    ApuestaDTO ap = new ApuestaDTO(reader.GetString(6), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDateTime(5).ToString());
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

        // MI BASE DE DATOS ESTA MAL MI CUOTA SE LLAMA DINERO
        internal int RetrieveCantidadApuestas(string USUARIO_email, double dinero)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            int cantApuestas = 0;
            command.CommandText = "SELECT * FROM `apuesta` WHERE `dinero` > "+dinero+" AND `USUARIO_email` LIKE '"+ USUARIO_email + "'";

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<Apuesta> apuestas = new List<Apuesta>();

                while (reader.Read())
                {
                    Apuesta ap = new Apuesta(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetMySqlDateTime(5).ToString(), reader.GetString(6));
                    //apuestas.Add(ap);
                    cantApuestas++;
                }
                connection.Close();
                return cantApuestas;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error al conectarse con la Base de Datos.");
                return -1;
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

            DateTime dt = DateTime.Now;
            string fechaApuesta = dt.ToString("yyyyMMddHHmmss");

            try
            {
                connection.Open();

                command.CommandText = "INSERT INTO apuesta (idApuesta, idMercado,tipo,cuota,dinero,fechaApuesta,USUARIO_email) " +
                "VALUES ('" + apuesta.IdApuesta + "' , '" + apuesta.IdMercado + "' , '" + apuesta.Tipo + "' ,'" + apuesta.Cuota + "' ,'" + apuesta.Dinero + "' ,'" + fechaApuesta + "' , '" + apuesta.Email + "' );";

                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e);
            }
        }

        internal void Operacion(int id)
        {
            MySqlConnection con = Conexion();
            MySqlCommand comand = con.CreateCommand();
            // Selecciona todo lo de mercados pasando la ID
            comand.CommandText = "select * from mercado WHERE IdMercado = '" + id + "';";
            Double probOver = 0;
            Double probUnder = 0;
            Double cuotaOver = 0;
            Double cuotaUnder = 0;
            Double Dinero_under = 0;
            Double Dinero_over = 0;
            try
            {
                con.Open();
                MySqlDataReader reader = comand.ExecuteReader();


                while (reader.Read())
                {
                    Dinero_over = reader.GetDouble(4);
                    Dinero_under = reader.GetDouble(5);
                }
                con.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Conn");
            }
            Debug.WriteLine(Dinero_under); Debug.WriteLine(Dinero_over);
            probOver = Dinero_over / (Dinero_over + Dinero_under);
            Debug.WriteLine(probOver);
            probUnder = Dinero_under / (Dinero_under + Dinero_over);
            cuotaOver = (1 / probOver) * 0.95;
            cuotaUnder = (1 / probUnder) * 0.95;
            Debug.WriteLine(probUnder); Debug.WriteLine(cuotaOver); Debug.WriteLine(cuotaUnder);


            comand.CommandText = "UPDATE `mercado` SET `cuotaOver` = '" + cuotaOver + "' , `cuotaUnder` = '" + cuotaUnder + "' WHERE `mercado`.`idMercado` = " + id;
            try
            {
                con.Open();
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Insert Apuesta");
            }

        }

        internal void ActualizarDinero(int id, double dinero, string overUnder)
        {

            double Dinero_over = 0;
            double Dinero_under = 0;

            MySqlConnection con = Conexion();
            MySqlCommand comand = con.CreateCommand();

            comand.CommandText = "select * from mercado WHERE IdMercado = '" + id + "';";
            try
            {
                con.Open();
                MySqlDataReader reader = comand.ExecuteReader();


                while (reader.Read())
                {
                    Dinero_over = reader.GetDouble(4);
                    Dinero_under = reader.GetDouble(5);
                }
                con.Close();
            }
            catch (Exception e)
            { 
                Debug.WriteLine("Error Conn");
            }

            Dinero_over += dinero;
            Dinero_under += dinero;

            if (overUnder == "over")
            {
                comand.CommandText = "UPDATE `mercado` SET `dineroOver` = '" + Dinero_over + "'  WHERE `mercado`.`idMercado` = " + id;
                try
                {
                    con.Open();
                    comand.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Insert Apuesta");
                }
            }

            if (overUnder == "under")
            {
                comand.CommandText = "UPDATE `mercado` SET `dineroUnder` = '" + Dinero_under + "'  WHERE `mercado`.`idMercado` = " + id;
                try
                {
                    con.Open();
                    comand.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error Insert Apuesta");
                }
            }
        }
    }
}