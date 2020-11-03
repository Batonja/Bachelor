using Common.ErrorObjects;
using Common.Models.Airline;
using DatabaseLayer;
using System;
using System.Collections.Generic;

namespace Business
{
    public class AirlineBusiness : IAirlineBusiness
    {
        IAirlineDatabase _airlineDatabase;

        public AirlineBusiness(IAirlineDatabase airlineDatabase)
        {
            _airlineDatabase = airlineDatabase;
        }

        public Holder<Destination> AddDestination(Destination destination)
        {
            List<Destination> destinationsFromDB = _airlineDatabase.GetDestinations();

            foreach (var destinationFromDB in destinationsFromDB)
            {
                if (destinationFromDB.Title == destination.Title)
                    return CheckDestination(destination, 400, "This destination already exists in database");
            }

            if (_airlineDatabase.AddDestination(destination))
                return CheckDestination(destination, 200, "");

            return CheckDestination(destination, 500, "Error while trying to add destination");
        }

        public List<Destination> GetDestinations()
        {
            return _airlineDatabase.GetDestinations();
        }

        public Holder<Airline> AddAirline(Airline airline)
        {
            Airline airlineFromDB = _airlineDatabase.Get(airline.AirlineId);

            if (airlineFromDB != null)
                return CheckAirline(airline, 404, "Airline you're trying to add already exists");


            if (_airlineDatabase.AddAirline(airline))
                return CheckAirline(airline, 200, "");

            return CheckAirline(airline, 500, "Error while trying to add airline");
        }

        public Holder<Airline> DeleteAirline(int airlineId)
        {
            Airline airlineFromDb = _airlineDatabase.Get(airlineId);

            if (airlineFromDb.AirlineId <= 0)
                return CheckAirline(new Airline(), 404, "Airline you'retrying to delete doesn't exists");

            if (_airlineDatabase.DeleteAirline(airlineId))
                return CheckAirline(new Airline(), 200, "");

            return CheckAirline(new Airline(), 400, "Unable to delete airline");
        }

        public Holder<Airline> EditAirline(Airline airline)
        {
            Airline airlineFromDB = _airlineDatabase.Get(airline.AirlineId);

            if (airlineFromDB.AirlineId <= 0)
                return CheckAirline(airline, 404, "Airline you're trying to edit doesn't exists");

            if (!_airlineDatabase.EditAirline(airline))
                return CheckAirline(airline, 500, "Unable to edit airline");


            


            if (airline.AvailableFlightLuggage != null)
            {
                foreach (var airlineAfl in airline.AvailableFlightLuggage)
                {
                    AirlineFlightLuggage aflToAdd = airlineAfl;

                    foreach (var airlineDbAfl in airlineFromDB.AvailableFlightLuggage)
                    {
                        if (airlineDbAfl.FlightLuggageId == airlineAfl.FlightLuggageId && airlineDbAfl.AirlineId == airlineAfl.AirlineId)
                        {
                            aflToAdd = null;
                            break;
                        }
                    }

                    if (aflToAdd != null)
                    {
                        if (!_airlineDatabase.AddAirlineFlightLuggage(aflToAdd))
                            return CheckAirline(airline, 500, "Unable to add AirlineFlightLuggage while editing airline");
                    }
                }
            }


            return CheckAirline(airline, 200, "");

        }

        public List<Airline> Get()
        {
            List<Airline> retVal = _airlineDatabase.Get();

            return retVal;
        }

        public Airline Get(int id)
        {
            return _airlineDatabase.Get(id);
        }

        #region 
        Holder<Airline> CheckAirline(Airline airline, int errorCode, string description) =>
            errorCode == 200 ? Holder<Airline>.Success(airline) : Holder<Airline>.Fail(errorCode, description);

        Holder<Destination> CheckDestination(Destination destination, int errorCode, string description) =>
            errorCode == 200 ? Holder<Destination>.Success(destination) : Holder<Destination>.Fail(errorCode, description);

        #endregion

    }
}
