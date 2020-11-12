using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendations.ViewModels;
using MoviesDataAccessLibrary.Entities;
using MoviesDataAccessLibrary.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRecommendations.Controllers
{
    public class PartyController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PartyController(IRepository repository,
                               IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("party/{userEmail}")]
        public IActionResult AllParties(string userEmail)
        {
            List<Party> userParties = _repository.GetUserParties(userEmail);
            List<PartyViewModel> userPartiesViewModel = _mapper.Map<List<Party>, List<PartyViewModel>>(userParties);
            
            return View(userPartiesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParty(string userEmail)
        {
            string partyName;
            try
            {
                partyName = Request.Form["partyName"];
            }
            catch (System.Exception)
            {
                partyName = "New Party";
            }
            Party newParty = new Party
            {
                Name = partyName,
                CreatorEmail = userEmail
            };
            await _repository.AddParty(newParty);

            PartyMember newPartyMember = new PartyMember
            {
                PartyId = newParty.Id,
                Email = newParty.CreatorEmail
            };
            await _repository.AddMemberToParty(newPartyMember);
            return RedirectToAction("AllParties", new { userEmail = userEmail });
        }

        [HttpGet]
        [Route("party/details/{partyId}")]
        public IActionResult Details(int partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            string partyName = party.Name.Replace(" ", "");

            // cookies for seen movies range
            
            if (HttpContext.Request.Cookies.ContainsKey($"{partyName}NewestMovieId") == false)
            {
                HttpContext.Response.Cookies.Append($"{partyName}NewestMovieId", "0");
            }
            
            if (HttpContext.Request.Cookies.ContainsKey($"{partyName}OldestMovieId") == false)
            {
                HttpContext.Response.Cookies.Append($"{partyName}OldestMovieId", "0");
            }
            
            Party partyFromDb = _repository.GetPartyById(partyId);
            return View(partyFromDb);
        }
    }
}
