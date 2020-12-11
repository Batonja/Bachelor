using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Common.ErrorObjects;
using Common.Models;
using FlightService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        IMediator _mediator;
        ILuggageLocationBusiness _luggageLocationBusiness;

        public FlightController(ILuggageLocationBusiness luggageLocationBusiness,IMediator mediator)
        {
            _mediator = mediator;
            _luggageLocationBusiness = luggageLocationBusiness;
        }

        [HttpPost]
        public Holder<FlightOrder> DeleteFlightOrder([FromBody]FlightOrder flightOrder)
        {
            Holder<FlightOrder> retVal = _luggageLocationBusiness.DeleteFlightOrder(flightOrder);
            return retVal;
        }

        [HttpPost]
        public async Task<IActionResult> UserIsCreated([FromBody]Guid guid)
        {
            var query = new UserIsCreatedQuery(guid);
            var result  = await _mediator.Send(query);
            return Ok(result);
            
        }

        [HttpGet]
        public List<FlightOrder> GetFlightOrders()
        {
            List<FlightOrder> orders = _luggageLocationBusiness.GetFlightOrders();

            return orders;
        }

        // GET: api/Airline
        [HttpGet]
        public List<Airline> Get()
        {

            List<Airline> retVal = _luggageLocationBusiness.Get();

            return retVal;
        }

        [HttpGet]
        public List<FlightLuggage> GetFlightLuggage()
        {
            return _luggageLocationBusiness.GetFlightLuggage();
        }

        [HttpGet("{id}", Name = "GetFlightLuggage")]
        public FlightLuggage GetFlightLuggage(int id)
        {
            return _luggageLocationBusiness.GetFlightLuggage(id);
        }

        // GET: api/Airline/5
        [HttpGet("{id}", Name = "Get")]
        public Airline Get(int id)
        {
            return _luggageLocationBusiness.Get(id);
        }

        // POST: api/Airline
        [HttpPost]
        public Holder<Airline> AddAirline([FromBody] Airline airline)
        {

            Holder<Airline> retValu = _luggageLocationBusiness.AddAirline(airline);



            return retValu;
        }



        [HttpPost]
        public Holder<Destination> AddDestination([FromBody] Destination destination)
        {
            Holder<Destination> retVal = _luggageLocationBusiness.AddDestination(destination);

            return retVal;
        }

        [HttpGet]
        public List<Destination> GetDestinations()
        {

            return _luggageLocationBusiness.GetDestinations();
        }


        [HttpPost]
        public Holder<FlightOrder> ConfirmFlight([FromBody]FlightOrder flightOrder)
        {
            Holder<FlightOrder> retVal = _luggageLocationBusiness.ConfirmFlight(flightOrder);

            return retVal;
        }

        [HttpPost]
        public Holder<FlightOrder> OrderFlight([FromBody]FlightOrder flightOrder)
        {

            Holder<FlightOrder> retVal = _luggageLocationBusiness.OrderFlight(flightOrder);

            return retVal;
        }


        [HttpPost]
        public Holder<Flight> AddFlight([FromBody]Flight flight)
        {
            Holder<Flight> retval = _luggageLocationBusiness.AddFlight(flight);

            return retval;
        }

        [HttpPost]
        public Holder<FlightLuggage> AddFlightLuggage([FromBody] FlightLuggage flightLuggage)
        {
            return _luggageLocationBusiness.AddFlightLuggage(flightLuggage);
        }

        [HttpPost]
        public List<Airline> Search([FromBody] SearchObject searchObject)
        {
            return _luggageLocationBusiness.Search(searchObject);
        }

        [HttpPost]
        public List<Airline> Filter([FromBody]FilterObject filterObject)
        {
            return _luggageLocationBusiness.Filter(filterObject);
        }

        // PUT: api/Airline/5
        [HttpPost]
        public Holder<Airline> EditAirline([FromBody] Airline airline)
        {
            return _luggageLocationBusiness.EditAirline(airline);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Holder<Airline> Delete(int id)
        {
            return _luggageLocationBusiness.DeleteAirline(id);
        }
    }
}
