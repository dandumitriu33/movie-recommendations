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
    [EnableCors("AllowAnyOrigin")]
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
        public string Get()
        {
            return "Value 1";
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
        public async Task<IActionResult> Post([FromBody] PartyDTO partyDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Bad request.");
            }
            Party tempParty = _mapper.Map<PartyDTO, Party>(partyDTO);
            Party addedParty = await _repository.AddParty(tempParty);

            // adding the creator as a member of the party
            await addCreatorToParty(addedParty);

            return Ok($"Party \"{addedParty.Name}\" was created successfully.");
        }

        private async Task addCreatorToParty(Party addedParty)
        {
            PartyMember newPartyMember = new PartyMember
            {
                PartyId = addedParty.Id,
                Email = addedParty.CreatorEmail
            };
            await _repository.AddMemberToParty(newPartyMember);
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

            List<Movie> batchFromDb = _repository.GetBatch(newestId, oldestId, limit);
            List<MovieDTO> batchDTO = _mapper.Map<List<Movie>, List<MovieDTO>>(batchFromDb);
            return Ok(batchDTO);
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

        // GET: api/<PartiesController>/partyCount/{partyId}
        [HttpGet]
        [Route("partyCount/{partyId}")]
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
        public IActionResult GetMatches(int partyId, int count)
        {
            List<PartyChoice> validChoices = _repository.GetMovieIdsForParty(partyId, count);
            List<MovieDTO> result = new List<MovieDTO>();
            foreach (PartyChoice choice in validChoices)
            {
                Movie movie = _repository.GetMovieByMovieId(choice.MovieId);
                MovieDTO tempMovie = _mapper.Map<Movie, MovieDTO>(movie);
                result.Add(tempMovie);
            }
            result.Reverse();
            return Ok(result);
        }

        // POST: api/<PartiesController>/partyMembers/{partyId}/addMember
        [HttpPost]
        [Route("addMember")]
        public async Task<IActionResult> AddMemberToParty(PartyMemberDTO partyMemberDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Bad request.", ModelState);
            }
            PartyMember newMember = new PartyMember
            {
                PartyId = partyMemberDTO.PartyId,
                Email = partyMemberDTO.Email
            };
            await _repository.AddMemberToParty(newMember);
            return Ok($"Party member \"{partyMemberDTO.Email}\" was added to party \"{partyMemberDTO.PartyId}\" successfully.");
        }

        private IActionResult BadRequest(string v, object modelState)
        {
            throw new NotImplementedException();
        }

        // GET api/<PartiesController>/getMembers/{partyId}
        [HttpGet]
        [Route("getMembers/{partyId}")]
        public IActionResult GetPartyMembers(int partyId)
        {
            List<PartyMember> partyMembers = _repository.GetPartyMembersForParty(partyId);
            List<PartyMemberDTO> partyMembersDTO = _mapper.Map<List<PartyMember>, List<PartyMemberDTO>>(partyMembers);
            return Ok(partyMembersDTO);
        }
    }
}
