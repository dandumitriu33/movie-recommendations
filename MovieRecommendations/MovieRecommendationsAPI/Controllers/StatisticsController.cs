﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRecommendationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IRepository _repository;

        public StatisticsController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<StatisticsController>
        [HttpGet]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetInventoryGenresCount()
        {
            List<GenreCount> genreCount = new List<GenreCount>();
            Dictionary<string, string> colors = new Dictionary<string, string>()
            {
                { "Action", "#8AFAAB" },
                { "Adventure", "brown" },
                { "Comedy", "yellow" },
                { "Crime", "blue" },
                { "Drama", "red" },
                { "Default", "white" },
                { "Fantasy", "#B87CDB" },
                { "Horror", "gray" },
                { "Mystery", "green" },
                { "Romance", "cyan" },
                { "Sci-Fi", "#580070" },
                { "Western", "orange" }
            };
            var dbGenreCount = _repository.GetGenreCount();
            foreach (var dbGenre in dbGenreCount)
            {
                GenreCount tempGenreCount = new GenreCount
                {
                    GenreName = dbGenre.GenreName,
                    Count = dbGenre.Count,
                    Color = colors[dbGenre.GenreName]
                };
                genreCount.Add(tempGenreCount);
            }
            List<GenreCount> sortedGenreCount = genreCount.OrderBy(g => g.Count).ToList();
            return Ok(sortedGenreCount);
        }

        // GET api/<StatisticsController>/community
        [HttpGet("community")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetCommunityGenresCount(int id)
        {
            Dictionary<string, string> colors = new Dictionary<string, string>()
            {
                { "Action", "#8AFAAB" },
                { "Adventure", "brown" },
                { "Comedy", "yellow" },
                { "Crime", "blue" },
                { "Drama", "red" },
                { "Default", "white" },
                { "Fantasy", "#B87CDB" },
                { "Horror", "gray" },
                { "Mystery", "green" },
                { "Romance", "cyan" },
                { "Sci-Fi", "#580070" },
                { "Western", "orange" }
            };
            var communityGenreScore = _repository.GetCommunityGenresScore();
            var groupedCommunityGenreScore = communityGenreScore
                                                            .GroupBy(s => s.GenreName)
                                                            .Select(s => new CommunityGenreScore
                                                            {
                                                                GenreName = s.First().GenreName,
                                                                Score = s.Sum(g => g.Score),
                                                                Color = "Default"
                                                            })
                                                            .ToList();
            List<CommunityGenreScore> result = new List<CommunityGenreScore>();
            foreach (var genre in groupedCommunityGenreScore.Where(g => g.Score > 0).OrderByDescending(g => g.Score).ToList())
            {
                CommunityGenreScore tempCommunityGenreScore = new CommunityGenreScore
                {
                    GenreName = genre.GenreName,
                    Score = genre.Score,
                    Color = colors[genre.GenreName]
                };
                result.Add(tempCommunityGenreScore);
            }
            return Ok(result);
        }

        // GET api/<StatisticsController>/community
        [HttpGet("inventory/horror")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetLatestHorrorMovies()
        {
            int chartSize = 6;
            List<Movie> latestHorrorMovies = _repository.GetLatestHorrorMovies(chartSize);
            return Ok(latestHorrorMovies);
        }

    }
}
