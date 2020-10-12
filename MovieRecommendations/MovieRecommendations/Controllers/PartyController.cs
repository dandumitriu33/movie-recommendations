﻿using System;
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

            // cookies for seen movies range
            //HttpContext.Response.Cookies.Append($"{partyName.Replace(" ", "")}NewestMovieId", "0");
            //HttpContext.Response.Cookies.Append($"{partyName.Replace(" ", "")}OldestMovieId", "0");

            PartyMember newPartyMember = new PartyMember
            {
                PartyId = newParty.Id,
                Email = newParty.CreatorEmail
            };
            _repository.AddMemberToParty(newPartyMember);
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