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
    public class Top20Controller : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public Top20Controller(IRepository repository,
                               IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET: api/<Top20Controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<Movie> top20MoviesFromDb = _repository.GetAllMoviesTop20().ToList();
            List<MovieDTO> top20MovieDTOs = _mapper.Map<List<Movie>, List<MovieDTO>>(top20MoviesFromDb);
            return Ok(top20MovieDTOs);
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
