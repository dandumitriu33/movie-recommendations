using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieRecommendations.Controllers
{
    public class PartyController : Controller
    {
        public IActionResult AllParties(string userEmail)
        {
            return View();
        }
    }
}
