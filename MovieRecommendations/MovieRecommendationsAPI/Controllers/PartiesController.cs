using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public PartiesController(IRepository repository)
        {
            _repository = repository;
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
            return Ok(userParties);
        }

        // POST api/<PartiesController>
        [HttpPost]
        public IActionResult Post([FromBody] Party party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The query is not formatted correctly");
            }
            _repository.AddParty(party);
            PartyMember newPartyMember = new PartyMember
            {
                PartyId = party.Id,
                Email = party.CreatorEmail
            };
            _repository.AddMemberToParty(newPartyMember);
            return NoContent();
        }

        // unuset at this time, using API via JS
        // POST api/<PartiesController>/addToParty/{partyId}
        [HttpPost]
        [Route("addToParty/{partyId}")]
        public IActionResult AddMember([FromBody] PartyMember partyMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The query is not formatted correctly");
            }
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

            List<Movie> batch = _repository.GetBatch(newestId, oldestId, limit);
            return Ok(batch);
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
            List<Movie> result = new List<Movie>();
            foreach (PartyChoice choice in validChoices)
            {
                Movie tempMovie = _repository.GetMovieByMovieId(choice.MovieId);
                result.Add(tempMovie);
            }
            result.Reverse();
            return Ok(result);
        }

        // POST: api/<PartiesController>/partyMembers/{partyId}/addMember/{memberEmail}
        [HttpPost]
        [Route("partyMembers/{partyId}/addMember/{memberEmail}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult AddMemberToParty(int partyId, string memberEmail)
        {
            PartyMember newMember = new PartyMember
            {
                Email = memberEmail,
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
            return Ok(partyMembers);
        }
    }
}
