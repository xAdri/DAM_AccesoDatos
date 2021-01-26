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
    public class UsuariosController : ApiController
    {
        // GET: api/Usuario/5
        [HttpGet]
        [ActionName("Get")]
        public List<Usuario> Get()
        {
            var repo = new UsuariosRepository();
            List<Usuario> usuario = repo.Retrieve();
            return usuario;
        }

        [HttpGet]
        [ActionName("GetDTO")]
        public List<Usuario> GetEmail(string email)
        {
            var repo = new UsuariosRepository();
            List<Usuario> usuario = repo.Retrieve();
            return usuario;
        }

        [HttpGet]
        [Route("api/Usuarios/Get/email={email}/tipo={tipo}")]
        public IEnumerable<Apuesta> GetTipo(string email, string tipo)
        {
            var repo = new UsuariosRepository();
            List<Apuesta> apuestas = repo.RetrieveApuestasEmail(email, tipo);
            return apuestas;
        }

        // POST: api/Usuario
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
