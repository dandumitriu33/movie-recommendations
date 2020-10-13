using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Models;

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
            List<GenreCountDTO> genreCount = new List<GenreCountDTO>();
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
                GenreCountDTO tempGenreCount = new GenreCountDTO
                {
                    GenreName = dbGenre.GenreName,
                    Count = dbGenre.Count,
                    Color = colors[dbGenre.GenreName]
                };
                genreCount.Add(tempGenreCount);
            }
            List<GenreCountDTO> sortedGenreCount = genreCount.OrderBy(g => g.Count).ToList();
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
                                                            .Select(s => new CommunityGenreScoreDTO
                                                            {
                                                                GenreName = s.First().GenreName,
                                                                Score = s.Sum(g => g.Score),
                                                                Color = "Default"
                                                            })
                                                            .OrderBy(g => g.Score)
                                                            .ToList();
            List<CommunityGenreScoreDTO> result = new List<CommunityGenreScoreDTO>();
            foreach (var genre in groupedCommunityGenreScore)
            {
                CommunityGenreScoreDTO tempCommunityGenreScore = new CommunityGenreScoreDTO
                {
                    GenreName = genre.GenreName,
                    Score = genre.Score,
                    Color = colors[genre.GenreName]
                };
                result.Add(tempCommunityGenreScore);
            }
            return Ok(result);
        }

    }
}
