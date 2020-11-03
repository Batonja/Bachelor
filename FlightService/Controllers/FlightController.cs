using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Common.ErrorObjects;
using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        IFlightBusiness _flightBusiness;

        public FlightController(IFlightBusiness flightBusiness)
        {
            _flightBusiness = flightBusiness;
        }

        [HttpPost]
        public Holder<FlightOrder> DeleteFlightOrder([FromBody]FlightOrder flightOrder)
        {
            Holder<FlightOrder> retVal = _flightBusiness.DeleteFlightOrder(flightOrder);
            return retVal;
        }

        [HttpGet]
        public List<FlightOrder> GetFlightOrders()
        {
            List<FlightOrder> orders = _flightBusiness.GetFlightOrders();

            return orders;
        }

        // GET: api/Airline
        [HttpGet]
        public List<Airline> Get()
        {

            List<Airline> retVal = _flightBusiness.Get();

            return retVal;
        }

        [HttpGet]
        public List<FlightLuggage> GetFlightLuggage()
        {
            return _flightBusiness.GetFlightLuggage();
        }

        [HttpGet("{id}", Name = "GetFlightLuggage")]
        public FlightLuggage GetFlightLuggage(int id)
        {
            return _flightBusiness.GetFlightLuggage(id);
        }

        // GET: api/Airline/5
        [HttpGet("{id}", Name = "Get")]
        public Airline Get(int id)
        {
            return _flightBusiness.Get(id);
        }

        // POST: api/Airline
        [HttpPost]
        public Holder<Airline> AddAirline([FromBody] Airline airline)
        {

            Holder<Airline> retValu = _flightBusiness.AddAirline(airline);



            return retValu;
        }



        [HttpPost]
        public Holder<Destination> AddDestination([FromBody] Destination destination)
        {
            Holder<Destination> retVal = _flightBusiness.AddDestination(destination);

            return retVal;
        }

        [HttpGet]
        public List<Destination> GetDestinations()
        {

            return _flightBusiness.GetDestinations();
        }


        [HttpPost]
        public Holder<FlightOrder> ConfirmFlight([FromBody]FlightOrder flightOrder)
        {
            Holder<FlightOrder> retVal = _flightBusiness.ConfirmFlight(flightOrder);

            return retVal;
        }

        [HttpPost]
        public Holder<FlightOrder> OrderFlight([FromBody]FlightOrder flightOrder)
        {

            Holder<FlightOrder> retVal = _flightBusiness.OrderFlight(flightOrder);

            return retVal;
        }


        [HttpPost]
        public Holder<Flight> AddFlight([FromBody]Flight flight)
        {
            Holder<Flight> retval = _flightBusiness.AddFlight(flight);

            return retval;
        }

        [HttpPost]
        public Holder<FlightLuggage> AddFlightLuggage([FromBody] FlightLuggage flightLuggage)
        {
            return _flightBusiness.AddFlightLuggage(flightLuggage);
        }

        [HttpPost]
        public List<Airline> Search([FromBody] SearchObject searchObject)
        {
            return _flightBusiness.Search(searchObject);
        }

        [HttpPost]
        public List<Airline> Filter([FromBody]FilterObject filterObject)
        {
            return _flightBusiness.Filter(filterObject);
        }

        // PUT: api/Airline/5
        [HttpPost]
        public Holder<Airline> EditAirline([FromBody] Airline airline)
        {
            return _flightBusiness.EditAirline(airline);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Holder<Airline> Delete(int id)
        {
            return _flightBusiness.DeleteAirline(id);
        }
    }
}
