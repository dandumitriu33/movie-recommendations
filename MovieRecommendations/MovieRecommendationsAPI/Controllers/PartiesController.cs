using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRecommendationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
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
    }
}
