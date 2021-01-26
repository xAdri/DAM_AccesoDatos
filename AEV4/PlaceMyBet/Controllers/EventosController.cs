using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    // No es necesario el route
    //[Route("api/Eventos/{action}")]
    public class EventosController : ApiController
    {
        // GET: api/Eventos/5
        public IEnumerable<Evento> Get()
        {
            var repo = new EventosRepository();
            List<Evento> evento = repo.Retrieve();
            return evento;
        }

        /*
        public IEnumerable<EventoDTO> GetDTO()
        {
            var repo = new EventosRepository();
            List<EventoDTO> evento = repo.RetrieveDTO();
            return evento;
        }*/

        // GET: api/Eventos?tipo={tipo}
        // Ejemplo : /localhost:44327/api/Eventos?tipo=1.5&idEvento=1
        public IEnumerable<Mercado> Get(string tipo, int idEvento)
        {
            Debug.WriteLine("ENTRO EN GET TIPO");
            var repo = new EventosRepository();
            List<Mercado> mercados = repo.RetriveTipoMercado(tipo, idEvento);
            return mercados;
        }

        // POST: api/Eventos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Eventos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Eventos/5
        public void Delete(int id)
        {
        }
    }
}
