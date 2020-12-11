using Common.ErrorObjects;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ILuggageLocation
    {
        List<Airline> Get();
        Airline Get(int id);
        List<Airline> Search(SearchObject searchObject);
        List<Airline> Filter(FilterObject filterObject);
        List<FlightOrder> GetFlightOrders();
        Task<int> UserIsCreated(Guid guid);
        Holder<FlightOrder> ConfirmFlight(FlightOrder flightOrder);
        Holder<FlightOrder> DeleteFlightOrder(FlightOrder flightOrder);
        Holder<FlightOrder> OrderFlight(FlightOrder flightOrder);
        Holder<Airline> AddAirline(Airline airline);
        Holder<Airline> EditAirline(Airline airline);
        Holder<Airline> DeleteAirline(int airlineId);
        Holder<Destination> AddDestination(Destination destination);
        Holder<Flight> AddFlight(Flight flight);
        List<Destination> GetDestinations();
        List<FlightLuggage> GetFlightLuggage();
        Holder<FlightLuggage> AddFlightLuggage(FlightLuggage flightLuggage);
        FlightLuggage GetFlightLuggage(int id);
    }
}
