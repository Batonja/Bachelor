using Common.Models.Airline;
using DatabaseLayer.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseLayer
{
    public class AirlineDatabase : IAirlineDatabase
    {
        public List<Airline> Get()
        {
            List<Airline> airlines = new List<Airline>();
            
            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                airlines = context.Airline.Include(airline => airline.AvailableFlightLuggage).
                    Include(airline => airline.AvailableFlightLuggage).ThenInclude(afl => afl.FlightLuggage).
                    Include(airline => airline.AirlineDestinations).ThenInclude(ad => ad.Destination).ToList();

            }



            return airlines;
        }

        public bool AddAirline(Airline airline)
        {

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {

                foreach (var afl in airline.AvailableFlightLuggage)
                    context.FlightLuggage.Attach(afl.FlightLuggage);

                foreach (var ad in airline.AirlineDestinations)
                    context.Destination.Attach(ad.Destination);

                context.AirlineDestination.AttachRange(airline.AirlineDestinations);

                context.Airline.Add(airline);
                int effectedRows = context.SaveChanges();

                if (effectedRows > 0)
                    return true;
            }

            return false;
        }

        public Airline Get(int id)
        {
            Airline airline = new Airline();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                airline = context.Airline.Include(theAirline => theAirline.AvailableFlightLuggage)
                    .Where(theAirline => theAirline.AirlineId == id).SingleOrDefault();

            }

            return airline;
        }

        public bool EditAirline(Airline airline)
        {


            int rowsEffected = -1;
            using (var context = new DataContext(DataContext.ops.dbOptions))
            {


                Airline airlineFromDB = context.Airline.Include(theAirline => theAirline.AvailableFlightLuggage)
                    .Include(theAirline => theAirline.AirlineDestinations)
                    .Where(theAirline => theAirline.AirlineId == airline.AirlineId).SingleOrDefault();

                if (airlineFromDB != null)
                {
                    context.Entry(airlineFromDB).CurrentValues.SetValues(airline);
                    context.Update(airlineFromDB);
                }


                rowsEffected = context.SaveChanges();
            }

            if (rowsEffected > 0)
                return true;

            return false;
        }

        public bool AddAirlineFlightLuggage(AirlineFlightLuggage airlineFlightLuggage)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                context.FlightLuggage.Attach(airlineFlightLuggage.FlightLuggage);
                context.Add(airlineFlightLuggage);
                rowsEffected = context.SaveChanges();
            }

            if (rowsEffected > 0)
                return true;

            return false;

        }

        public bool DeleteAirline(int airlineId)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                Airline airlineFromDB = context.Airline.SingleOrDefault(airline => airline.AirlineId == airlineId);

                context.Airline.Remove(airlineFromDB);

                rowsEffected = context.SaveChanges();
            }

            if (rowsEffected > 0)
                return true;

            return false;
        }

        public bool AddDestination(Destination destination)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                context.Add(destination);
                rowsEffected = context.SaveChanges();

            }

            if (rowsEffected > 0)
                return true;

            return false;
        }

        public List<Destination> GetDestinations()
        {
            List<Destination> destinations = new List<Destination>();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                destinations = context.Destination.ToList();

            }

            return destinations;
        }






    }
}
