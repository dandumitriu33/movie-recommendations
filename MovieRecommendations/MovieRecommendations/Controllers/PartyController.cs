using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Models;

namespace MovieRecommendations.Controllers
{
    public class PartyController : Controller
    {
        private readonly IRepository _repository;

        public PartyController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("party/{userEmail}")]
        public IActionResult AllParties(string userEmail)
        {
            List<Party> userParties = _repository.GetUserParties(userEmail);
            return View(userParties);
        }

        [HttpPost]
        public IActionResult CreateParty(string userEmail)
        {
            string partyName = Request.Form["partyName"];
            Party newParty = new Party
            {
                Name = partyName,
                CreatorEmail = userEmail
            };
            _repository.AddParty(newParty);
            PartyMember newPartyMember = new PartyMember
            {
                PartyId = newParty.Id,
                Email = newParty.CreatorEmail
            };
            _repository.AddMemberToParty(newPartyMember);
            return RedirectToAction("AllParties", new { userEmail = userEmail });
        }
    }
}
