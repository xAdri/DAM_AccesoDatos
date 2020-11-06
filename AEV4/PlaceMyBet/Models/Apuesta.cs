using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Apuesta
    {
        public int IdApuesta { get; set; }
        public int IdMercado { get; set; }
        public string Tipo { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public string FechaApuesta { get; set; }
        public string Email { get; set; }

        public Apuesta(int idApuesta, int idMercado, string tipo, double cuota, double dinero, string fechaApuesta, string email)
        {
            IdApuesta = idApuesta;
            IdMercado = idMercado;
            Tipo = tipo;
            Cuota = cuota;
            Dinero = dinero;
            FechaApuesta = fechaApuesta;
            Email = email;
        }
    }

    public class ApuestaDTO
    {
        public string Email { get; set; }
        public string Tipo { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public string FechaApuesta { get; set; }

        public ApuestaDTO(string email,string tipo, double cuota, double dinero, string fechaApuesta)
        {
            Email = email;
            Tipo = tipo;
            Cuota = cuota;
            Dinero = dinero;
            FechaApuesta = fechaApuesta;
        }
    }
}