﻿using System;
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
    public class Top20Controller : ControllerBase
    {
        private readonly IRepository _repository;

        public Top20Controller(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<Top20Controller>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Movie> top20MoviesFromDb = _repository.GetAllMoviesTop20();
            List<MovieDTO> top20MovieDTOs = new List<MovieDTO>();
            foreach (var movie in top20MoviesFromDb)
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
                top20MovieDTOs.Add(tempMovie);
            }
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
