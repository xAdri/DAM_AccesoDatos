using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Apuesta
    {
        public int IdApuesta { get; set; }
        public double Tipo { get; set; }
        public double Cuota { get; set; }
        public double Dinero { get; set; }
        public DateTime FechaApuesta { get; set; }

        public Apuesta(int idApuesta, double tipo, double cuota, double dinero, DateTime fechaApuesta)
        {
            IdApuesta = idApuesta;
            Tipo = tipo;
            Cuota = cuota;
            Dinero = dinero;
            FechaApuesta = fechaApuesta;
        }
    }
}