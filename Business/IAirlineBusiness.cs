using Common.ErrorObjects;
using Common.Models.Airline;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IAirlineBusiness
    {
        List<Airline> Get();
        Airline Get(int id);
        Holder<Airline> AddAirline(Airline airline);
        Holder<Airline> EditAirline(Airline airline);
        Holder<Airline> DeleteAirline(int airlineId);
        Holder<Destination> AddDestination(Destination destination);
        List<Destination> GetDestinations();
    }
}
