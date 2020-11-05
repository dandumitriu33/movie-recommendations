using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendationsAPI.Models;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRecommendationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository _repository;

        public MoviesController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<AllMoviesController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Movie> allMoviesFromDb = _repository.GetAllMovies();
            List<MovieDTO> allMovieDTOs = new List<MovieDTO>();
            foreach (var movie in allMoviesFromDb)
            {
                MovieDTO tempMovie = new MovieDTO
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    LengthInMinutes = movie.LengthInMinutes,
                    ReleaseYear = movie.ReleaseYear,
                    Rating = movie.Rating,
                    MainGenre = movie.MainGenre,
                    SubGenre1 = movie.SubGenre1,
                    SubGenre2 = movie.SubGenre2
                };
                allMovieDTOs.Add(tempMovie);
            }
            return Ok(allMovieDTOs);
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
