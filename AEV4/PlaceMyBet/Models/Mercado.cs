using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Mercado
    {
        public int IdMercado { get; set; }
        public string OverUnder { get; set; }
        public double CuotaOver { get; set; }
        public double CuotaUnder { get; set; }
        public double DineroOver { get; set; }
        public double DineroUnder { get; set; }

        public Mercado(int idMercado, string overUnder, double cuotaOver, double cuotaUnder, double dineroOver, double dineroUnder)
        {
            IdMercado = idMercado;
            OverUnder = overUnder;
            CuotaOver = cuotaOver;
            CuotaUnder = cuotaUnder;
            DineroOver = dineroOver;
            DineroUnder = dineroUnder;
        }
    }

    public class MercadoDTO
    {
        public string OverUnder { get; set; }
        public double CuotaOver { get; set; }
        public double CuotaUnder { get; set; }

        public MercadoDTO(string overUnder, double cuotaOver, double cuotaUnder)
        {
            OverUnder = overUnder;
            CuotaOver = cuotaOver;
            CuotaUnder = cuotaUnder;
        }
    }
}