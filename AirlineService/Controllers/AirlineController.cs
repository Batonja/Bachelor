using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Common.ErrorObjects;
using Common.Models.Airline;
using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {

        IAirlineBusiness _airlineBusiness;

        public AirlineController(IAirlineBusiness airlineBusiness)
        {
            _airlineBusiness = airlineBusiness;
        }



        [HttpGet]
        public List<Airline> Get()
        {

            List<Airline> retVal = _airlineBusiness.Get();

            return retVal;
        }

        // GET: api/Airline/5
        [HttpGet("{id}", Name = "Get")]
        public Airline Get(int id)
        {
            return _airlineBusiness.Get(id);
        }


        // POST: api/Airline
        [HttpPost]
        public Holder<Airline> AddAirline([FromBody] Airline airline)
        {

            Holder<Airline> retValu = _airlineBusiness.AddAirline(airline);



            return retValu;
        }

        [HttpPost]
        public Holder<Destination> AddDestination([FromBody] Destination destination)
        {
            Holder<Destination> retVal = _airlineBusiness.AddDestination(destination);

            return retVal;
        }

        [HttpGet]
        public List<Destination> GetDestinations()
        {

            return _airlineBusiness.GetDestinations();
        }

        // PUT: api/Airline/5
        [HttpPost]
        public Holder<Airline> EditAirline([FromBody] Airline airline)
        {
            return _airlineBusiness.EditAirline(airline);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Holder<Airline> Delete(int id)
        {
            return _airlineBusiness.DeleteAirline(id);
        }
    }
}
