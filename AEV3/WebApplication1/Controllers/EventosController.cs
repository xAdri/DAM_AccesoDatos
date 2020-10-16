using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Eventos/{action}")]
    public class EventosController : ApiController
    {
        // GET: api/Eventos/5
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Evento> Get()
        {
            var repo = new EventosRepository();
            List<Evento> evento = repo.Retrieve();
            return evento;
        }

        [HttpGet]
        [ActionName("GetDTO")]
        public IEnumerable<EventoDTO> GetDTO()
        {
            var repo = new EventosRepository();
            List<EventoDTO> evento = repo.RetrieveDTO();
            return evento;
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
