using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRecommendationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Top20Controller : ControllerBase
    {
        private readonly IRepository _repository;

        public Top20Controller(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<Top20Controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            IEnumerable<Movie> allMoviesFromDb = _repository.GetAllMoviesTop20();
            string movieCount = allMoviesFromDb.Count().ToString();
            return new string[] { "value1", "value2", movieCount };
        }

        // GET api/<Top20Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Top20Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Top20Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Top20Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
