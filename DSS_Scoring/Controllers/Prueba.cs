﻿using DSS_Scoring.Data;
using DSS_Scoring.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Prueba : ControllerBase
    {

        private MyDbContext _context;
        public Prueba(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/<Prueba>
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            var usuario = _context.Usuarios;
            
            return usuario;

        }

        // GET api/<Prueba>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Prueba>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Prueba>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Prueba>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
