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
    public class AllMoviesController : ControllerBase
    {
        private readonly IRepository _repository;

        public AllMoviesController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<AllMoviesController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Movie> allMoviesFromDb = _repository.GetAllMovies();
            string movieCount = allMoviesFromDb.Count().ToString();
            var result = new
            {
                sendTime = DateTime.UtcNow.ToString(),
                movies = allMoviesFromDb
            };
            return Ok(result);
        }

        // GET api/<AllMoviesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AllMoviesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AllMoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AllMoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
