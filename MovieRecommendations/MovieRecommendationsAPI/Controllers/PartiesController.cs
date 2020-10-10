using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesDataAccessLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRecommendationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PartiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
