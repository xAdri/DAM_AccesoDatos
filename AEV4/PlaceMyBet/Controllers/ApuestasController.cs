﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Apuestas/{action}")]
    public class ApuestasController : ApiController
    {
        // GET: api/Apuestas/5
        [HttpGet]
        [ActionName("Get")]
        public IEnumerable<Apuesta> Get()
        {
            var repo = new ApuestasRepository();
            List<Apuesta> apuesta = repo.Retrieve();
            return apuesta;
        }

        [HttpGet]
        [ActionName("GetDTO")]
        public IEnumerable<ApuestaDTO> GetDTO()
        {
            var repo = new ApuestasRepository();
            List<ApuestaDTO> apuesta = repo.RetrieveDTO();
            return apuesta;
        }

        // POST: api/Apuestas
        public void Post([FromBody]Apuesta value)
        {
            var repo = new ApuestasRepository();
            repo.Save(value);
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
