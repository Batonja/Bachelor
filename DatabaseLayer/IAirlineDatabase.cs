using Common.Models.Airline;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
    public interface IAirlineDatabase
    {
        Airline Get(int id);
        List<Airline> Get();
        bool EditAirline(Airline airline);
        bool AddAirline(Airline airline);
        bool DeleteAirline(int airlineId);
        bool AddDestination(Destination destination);
        bool AddAirlineFlightLuggage(AirlineFlightLuggage airlineFlightLuggage);
        List<Destination> GetDestinations();
    }
}
