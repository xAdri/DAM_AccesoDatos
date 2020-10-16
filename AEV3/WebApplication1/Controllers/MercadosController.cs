using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Mercados/{action}")]
    public class MercadosController : ApiController
    {
        // GET: api/Mercados/5
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Mercado> Get()
        {
            var repo = new MercadosRepository();
            List<Mercado> mercado = repo.Retrieve();
            return mercado;
        }

        [HttpGet]
        [ActionName("GetDTO")]
        public IEnumerable<MercadoDTO> GetDTO()
        {
            var repo = new MercadosRepository();
            List<MercadoDTO> mercado = repo.RetrieveDTO();
            return mercado;
        }

        // POST: api/Mercados
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mercados/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mercados/5
        public void Delete(int id)
        {
        }
    }
}
