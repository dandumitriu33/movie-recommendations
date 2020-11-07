using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendationsAPI.Models;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRecommendationsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PartiesController(IRepository repository,
                                 IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET: api/<PartiesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PartiesController>/john@email.com
        [HttpGet("{userEmail}")]
        public IActionResult Get(string userEmail)
        {
            List<Party> userParties = _repository.GetUserParties(userEmail);
            List<PartyDTO> userPartiesDTOs = new List<PartyDTO>();
            foreach (var party in userParties)
            {
                PartyDTO tempParty = _mapper.Map<Party, PartyDTO>(party);
                userPartiesDTOs.Add(tempParty);
            }
            return Ok(userPartiesDTOs);
        }

        // POST api/<PartiesController>
        [HttpPost]
        public IActionResult Post([FromBody] PartyDTO partyDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The query is not formatted correctly");
            }
            Party tempParty = new Party
            {
                Id = partyDTO.Id,
                Name = partyDTO.Name,
                CreatorEmail = partyDTO.CreatorEmail
            };
            _repository.AddParty(tempParty);

            // adding the creator as a member of the party
            PartyMember newPartyMember = new PartyMember
            {
                PartyId = partyDTO.Id,
                Email = partyDTO.CreatorEmail
            };
            _repository.AddMemberToParty(newPartyMember);
            return NoContent();
        }

        // unused at this time, using API via JS
        // POST api/<PartiesController>/addToParty/{partyId}
        [HttpPost]
        [Route("addToParty/{partyId}")]
        public IActionResult AddMember([FromBody] PartyMemberDTO partyMemberDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The query is not formatted correctly");
            }
            PartyMember partyMember = new PartyMember
            {
                Id = partyMemberDTO.Id,
                PartyId = partyMemberDTO.PartyId,
                Email = partyMemberDTO.Email
            };
            _repository.AddMemberToParty(partyMember);
            return NoContent();
        }

        // DELETE api/<PartiesController>/RemoveFromParty/{partyId}
        [HttpDelete("removeFromParty/{partyId}/{userEmail}")]
        public IActionResult RemoveMember(int partyId, string userEmail)
        {
            PartyMember partyMemberToRemove = _repository.GetPartyMember(partyId, userEmail);
            _repository.RemoveMemberFromParty(partyMemberToRemove);
            return NoContent();
        }

        // DELETE api/<PartiesController>/resetChoices/{partyId}
        [HttpDelete]
        [Route("resetChoices/{partyId}")]
        public IActionResult ResetChoices(int partyId)
        {
            _repository.ResetChoicesForParty(partyId);
            return NoContent();
        }

        // GET: api/<PartiesController>/
        [HttpGet]
        [Route("getBatchBefore/{newestId}/andAfter/{oldestId}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetSwiperBatch(int newestId, int oldestId)
        {
            // how many movies
            int limit = 10;

            List<Movie> batchFromDb = _repository.GetBatch(newestId, oldestId, limit);
            List<MovieDTO> batchDTO = new List<MovieDTO>();
            foreach (var movie in batchFromDb)
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
                batchDTO.Add(tempMovie);
            }
            return Ok(batchDTO);
        }

        // POST: api/<PartiesController>/partyChoices/{partyId}/choice/{movieId}
        [HttpPost]
        [Route("partyChoices/{partyId}/choice/{movieId}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult AddChoice(int partyId, int movieId)
        {
            PartyChoice newChoice = new PartyChoice
            {
                PartyId = partyId,
                MovieId = movieId,
                Score = 1
            };
            _repository.AddChoice(newChoice);
            return NoContent();
        }

        // GET: api/<PartiesController>/partyCount/{partyId}
        [HttpGet]
        [Route("partyCount/{partyId}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetPartyCount(int partyId)
        {
            int partyCount = _repository.GetPartyCount(partyId);
            PartyCount result = new PartyCount 
            { 
                partyId = partyId, 
                partyCount = partyCount 
            };
            return Ok(result);
        }

        // GET: api/<PartiesController>/getMatches/{partyId}/count/{count}
        [HttpGet]
        [Route("getMatches/{partyId}/count/{count}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetMatches(int partyId, int count)
        {
            List<PartyChoice> validChoices = _repository.GetMovieIdsForParty(partyId, count);
            List<MovieDTO> result = new List<MovieDTO>();
            foreach (PartyChoice choice in validChoices)
            {
                Movie movie = _repository.GetMovieByMovieId(choice.MovieId);
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
                result.Add(tempMovie);
            }
            result.Reverse();
            return Ok(result);
        }

        // POST: api/<PartiesController>/partyMembers/{partyId}/addMember/{memberEmail}
        [HttpPost]
        [Route("partyMembers/{partyId}/addMember/{memberEmail}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult AddMemberToParty(int partyId, MemberEmailDTO memberEmailDTO)
        {
            PartyMember newMember = new PartyMember
            {
                Email = memberEmailDTO.Email,
                PartyId = partyId
            };
            _repository.AddMemberToParty(newMember);
            return NoContent();
        }

        // GET api/<PartiesController>/getMembers/{partyId}
        [HttpGet]
        [Route("getMembers/{partyId}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GetPartyMembers(int partyId)
        {
            List<PartyMember> partyMembers = _repository.GetPartyMembersForParty(partyId);
            List<PartyMemberDTO> partyMembersDTO = new List<PartyMemberDTO>();
            foreach (var member in partyMembers)
            {
                PartyMemberDTO tempPartyMemberDTO = new PartyMemberDTO
                {
                    Id = member.Id,
                    Email = member.Email,
                    PartyId = member.PartyId
                };
                partyMembersDTO.Add(tempPartyMemberDTO);
            }
            return Ok(partyMembersDTO);
        }
    }
}
