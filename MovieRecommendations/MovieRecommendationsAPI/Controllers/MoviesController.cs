using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public MoviesController(IRepository repository,
                                IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET: api/<AllMoviesController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Movie> allMoviesFromDb = _repository.GetAllMovies().ToList();
            List<MovieDTO> allMovieDTOs = _mapper.Map<List<Movie>, List<MovieDTO>>(allMoviesFromDb);
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
