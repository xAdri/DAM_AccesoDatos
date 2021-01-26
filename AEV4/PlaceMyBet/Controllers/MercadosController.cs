using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Route("api/Mercados/{action}")]
    public class MercadosController : ApiController
    {
        // GET: api/Mercados/5
        public IEnumerable<Mercado> Get()
        {
            var repo = new MercadosRepository();
            List<Mercado> mercado = repo.Retrieve();
            return mercado;
        }

        public IEnumerable<MercadoDTO> GetDTO()
        {
            var repo = new MercadosRepository();
            List<MercadoDTO> mercado = repo.RetrieveDTO();
            return mercado;
        }

        //[Route("api/Mercados/Get/cuota={cuota}/email={email}")]
        // localhost:44327/api/Mercados?tipo=1.5&email=1
        public IEnumerable<Apuesta> GetEmail(double cuota, string email)
        {
            var repo = new MercadosRepository();
            List<Apuesta> mercados = repo.ApuestasUsuario(cuota, email);
            return mercados;
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
