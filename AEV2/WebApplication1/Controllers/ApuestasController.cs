using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ApuestasController : ApiController
    {
        // GET: api/Apuestas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Apuestas/5
        public List<Apuesta> Get(int id)
        {
            var repo = new ApuestasRepository();
            List<Apuesta> apuesta = repo.Retrieve();
            return apuesta;
        }

        // POST: api/Apuestas
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Apuestas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apuestas/5
        public void Delete(int id)
        {
        }
    }
}
