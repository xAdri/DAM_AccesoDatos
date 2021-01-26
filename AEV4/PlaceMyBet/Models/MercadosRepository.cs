﻿using System;
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

                    mercados.Add(new MercadoDTO(overUnder, cuotaOver, cuotaUnder));
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error connection");
            }
            return mercados;
        }

        internal List<Mercado> RetrieveMercadoOverUnder(string tipo)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM MERCADO where overUnder = @tipo";
            command.Parameters.AddWithValue("@tipo", tipo);

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

        internal List<Apuesta> ApuestasUsuario(double cuota, string email)
        {
            MySqlConnection connection = Conexion();
            MySqlCommand command = connection.CreateCommand();
            //command.CommandText = "SELECT * FROM `apuesta` WHERE `cuota` = '" + cuota + "' AND `USUARIO_email` LIKE '" + email + "'";
            command.CommandText = "SELECT* FROM `apuesta` WHERE `cuota` = 1.5 AND `USUARIO_email` LIKE 'adriperez@gmail.com'";
            

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<Apuesta> mercados = new List<Apuesta>();

                while (reader.Read())
                {
                    Apuesta me = new Apuesta(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetString(5), reader.GetString(6));
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
    }
}