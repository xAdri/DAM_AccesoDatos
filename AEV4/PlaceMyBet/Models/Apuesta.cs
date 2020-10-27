using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Apuesta
    {
        public int IdApuesta { get; set; }
        public string Tipo { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public DateTime FechaApuesta { get; set; }

        public Apuesta(int idApuesta, string tipo, double cuota, double dinero, DateTime fechaApuesta)
        {
            IdApuesta = idApuesta;
            Tipo = tipo;
            Cuota = cuota;
            Dinero = dinero;
            FechaApuesta = fechaApuesta;
        }
    }

    public class ApuestaDTO
    {

        public string Tipo { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public DateTime FechaApuesta { get; set; }

        public ApuestaDTO(string tipo, double cuota, double dinero, DateTime fechaApuesta)
        {
            Tipo = tipo;
            Cuota = cuota;
            Dinero = dinero;
            FechaApuesta = fechaApuesta;
        }
    }
}