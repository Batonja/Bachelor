using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Common.Events;
using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IEventsBusiness _eventsBusiness;

        public UserController(IEventsBusiness eventsBusiness)
        {
            _eventsBusiness = eventsBusiness;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        public async Task<int>Test()
        {
            User user = new User
            {
                Address = "adresa",
                Email = "mejl",
                FirstName = "ime",
                UserId = 123
            };

            UserAdded ua = new UserAdded { User = user };

            _eventsBusiness.TestEntry(ua);
            return 0;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
