using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public DateTime FechaEvento { get; set; }

        public Evento(int idEvento, string equipoLocal, string equipoVisitante, DateTime fechaEvento)
        {
            IdEvento = idEvento;
            EquipoLocal = equipoLocal;
            EquipoVisitante = equipoVisitante;
            FechaEvento = fechaEvento;
        }

    }

    public class EventoDTO
    {
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public DateTime FechaEvento { get; set; }

        public EventoDTO(string equipoLocal, string equipoVisitante, DateTime fechaEvento)
        {
            EquipoLocal = equipoLocal;
            EquipoVisitante = equipoVisitante;
            FechaEvento = fechaEvento;
        }
    }
}