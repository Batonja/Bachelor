using Common.Models;
using DatabaseLayer.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLayer
{
    public class FlightDatabase : IFlightDatabase
    {
        public List<Airline> Get()
        {
            List<Airline> airlines = new List<Airline>();
            List<Flight> flights = new List<Flight>();
            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                airlines = context.Airline.Include(airline => airline.AvailableFlightLuggage).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Tickets).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.FlightOrders).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Seats).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.ToDestination).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.FromDestination).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Airline).
                    Include(airline => airline.AvailableFlightLuggage).ThenInclude(afl => afl.FlightLuggage).
                    Include(airline => airline.AirlineDestinations).ThenInclude(ad => ad.Destination).ToList();

            }



            return airlines;
        }

        public List<Airline> SearchWithDestination(SearchObject searchObject)
        {
            List<Airline> retVal = new List<Airline>();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                retVal = context.Airline.Include(airline => airline.AvailableFlightLuggage).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Tickets).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.FlightOrders).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Seats).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.ToDestination).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.FromDestination).
                    Include(airline => airline.AirlineDestinations).ThenInclude(ad => ad.Destination)
                    .Where(dbAirline => dbAirline.Flights.Any(flight => flight.ToDestination.DestinationId == searchObject.Destination.DestinationId
                    && flight.ArrivalDate == searchObject.ArrivalDate.Date
                    && flight.DepartureDate == searchObject.DepartureDate.Date
                    && flight.Tickets.Any(ticket => ticket.Price >= searchObject.PriceRange[0]
                    && ticket.Price <= searchObject.PriceRange[1]))).ToList();


            }

            return retVal;
        }

        public List<Airline> Search(SearchObject searchObject)
        {
            List<Airline> retVal = new List<Airline>();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                retVal = context.Airline.Include(airline => airline.AvailableFlightLuggage).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Tickets).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.FlightOrders).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.Seats).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.ToDestination).
                    Include(airline => airline.Flights).ThenInclude(flight => flight.FromDestination).
                    Include(airline => airline.AirlineDestinations).ThenInclude(ad => ad.Destination)
                    .Where(dbAirline => dbAirline.Flights.Any(flight => flight.ArrivalDate == searchObject.ArrivalDate.Date
                    && flight.DepartureDate == searchObject.DepartureDate.Date
                    && flight.Tickets.Any(ticket => ticket.Price >= searchObject.PriceRange[0]
                    || ticket.Price <= searchObject.PriceRange[1]))).ToList();


            }

            return retVal;

        }
        public List<Airline> Filter(List<Airline> airlines)
        {
            List<Airline> retVal = new List<Airline>();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                foreach (var airline in airlines)
                {

                    retVal = retVal.Concat(context.Airline.Include(dbAirline => dbAirline.Flights).ThenInclude(dbFlight => dbFlight.Seats)
                    .Include(dbAirline => dbAirline.Flights).ThenInclude(dbFlight => dbFlight.Tickets)
                    .Include(dbAirline => dbAirline.Flights).ThenInclude(dbFlight => dbFlight.FlightOrders)
                    .Include(dbAirline => dbAirline.Flights).ThenInclude(dbFlight => dbFlight.FromDestination)
                    .Include(dbAirline => dbAirline.Flights).ThenInclude(dbFlight => dbFlight.ToDestination)
                    .Where(dbAirline => dbAirline.AirlineId == airline.AirlineId).ToList()).ToList();

                }
            }
            return retVal;

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
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.ToDestination)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.FromDestination)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.Tickets)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.Seats).Include(theAirline => theAirline.AirlineDestinations)
                    .Where(theAirline => theAirline.AirlineId == id).SingleOrDefault();

            }

            return airline;
        }

        public bool FreeSeat(Seat seat)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                seat = context.Seat.Where(theSeat => theSeat.SeatId == seat.SeatId).FirstOrDefault();

                seat.SeatState = Common.Enums.SeatState.FREE;

                context.Seat.Update(seat);

                rowsEffected = context.SaveChanges();
            }

            return rowsEffected > 0 ? true : false;
        }

        public bool DeleteFlightOrder(FlightOrder flightOrder)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                flightOrder = context.FlightOrder.Where(theFlightOrder =>
                    theFlightOrder.FlightOrderId == flightOrder.FlightOrderId).FirstOrDefault();
                context.FlightOrder.Remove(flightOrder);
                rowsEffected = context.SaveChanges();
            }

            return rowsEffected > 0 ? true : false;
        }

        public bool ConfirmFlight(FlightOrder flightOrder)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                flightOrder = context.FlightOrder.Where(theFlightOrder => theFlightOrder.FlightOrderId == flightOrder.FlightOrderId).SingleOrDefault();
                flightOrder.Confirmed = true;

                context.Update(flightOrder);
                rowsEffected = context.SaveChanges();
            }

            return rowsEffected > 0 ? true : false;
        }

        public List<FlightOrder> GetFlightOrders()
        {
            List<FlightOrder> flightOrders = new List<FlightOrder>();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                flightOrders = context.FlightOrder
                    .Include(theFlightOrder => theFlightOrder.Flight).ThenInclude(theFlight => theFlight.ToDestination)
                    .Include(theFlightOrder => theFlightOrder.Flight).ThenInclude(theFlight => theFlight.FromDestination)
                    .Include(theFlightOrder => theFlightOrder.Flight).ThenInclude(theFlight => theFlight.Airline)
                    .Include(theFlightOrder => theFlightOrder.FlightLuggage)
                    .Include(theFlightOrder => theFlightOrder.FlightTicket)
                    .Include(theFlightOrder => theFlightOrder.Seat)
                    .Include(theFlightOrder => theFlightOrder.User).ToList();
            }

            return flightOrders;

        }

        public bool AddFlightOrder(FlightOrder flightOrder)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                User user = context.User.Where(theUser => theUser.UserId == flightOrder.User.UserId).SingleOrDefault();
                Flight flight = context.Flight.Where(theFlight => theFlight.FlightId == flightOrder.Flight.FlightId).SingleOrDefault();
                FlightLuggage flightLuggage = context.FlightLuggage.Where(theFlightLuggage => theFlightLuggage.FlightLuggageId == flightOrder.FlightLuggage.FlightLuggageId).SingleOrDefault();
                Seat seat = context.Seat.Where(theSeat => theSeat.SeatId == flightOrder.Seat.SeatId).SingleOrDefault();
                FlightTicket flightTicket = context.FlightTicket.Where(theFlightTicket => theFlightTicket.FlightTicketId == flightOrder.FlightTicket.FlightTicketId).SingleOrDefault();

                flightOrder.User = user;
                flightOrder.Flight = flight;
                flightOrder.FlightLuggage = flightLuggage;
                flightOrder.Seat = seat;
                flightOrder.FlightTicket = flightTicket;





                context.FlightOrder.Add(flightOrder);

                rowsEffected = context.SaveChanges();

                if (rowsEffected > 0)
                    return true;

            }

            return false;
        }

        public FlightTicket AddTicket(FlightTicket flightTicket)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                FlightTicket ticket = context.FlightTicket.Add(flightTicket).Entity;

                rowsEffected = context.SaveChanges();

                if (rowsEffected > 0)
                    return ticket;
            }

            return null;
        }

        public User GetUserByPassportId(long passportId)
        {
            User user = new User();
            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                user = context.User.Where(userDb => userDb.PassportId == passportId).SingleOrDefault();
            }

            return user;
        }

        public Seat EditSeat(Seat seat)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                Seat seatFromDb = context.Seat.Include(theSeat => theSeat.Flight).Where(theSeat => theSeat.SeatId == seat.SeatId)
                    .SingleOrDefault();

                if (seatFromDb != null)
                {
                    context.Entry(seatFromDb).CurrentValues.SetValues(seat);
                    context.Update(seatFromDb);
                }

                rowsEffected = context.SaveChanges();

                if (rowsEffected > 0)
                    return seatFromDb;
            }
            return null;

        }

        public bool EditFlight(Flight flight)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                Flight flightFromDb = context.Flight.Include(theFlight => theFlight.Seats).Where(theFlight => theFlight.FlightId == flight.FlightId).SingleOrDefault();

                if (flightFromDb != null)
                {
                    context.Entry(flightFromDb).CurrentValues.SetValues(flight);
                    context.Update(flightFromDb);
                }

                rowsEffected = context.SaveChanges();

            }

            return rowsEffected > 0 ? true : false;
        }

        public bool AddSeat(Seat seat)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                Flight flight = context.Flight.Where(theFlight => theFlight.FlightId == seat.Flight.FlightId).SingleOrDefault();
                seat.Flight = flight;
                context.Add(seat);

                rowsEffected = context.SaveChanges();
            }

            return rowsEffected > 0 ? true : false;
        }

        public bool EditAirline(Airline airline)
        {


            int rowsEffected = -1;
            using (var context = new DataContext(DataContext.ops.dbOptions))
            {


                Airline airlineFromDB = context.Airline.Include(theAirline => theAirline.AvailableFlightLuggage)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.ToDestination)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.FromDestination)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.Tickets)
                    .Include(theAirline => theAirline.Flights).ThenInclude(flight => flight.Seats)
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

        public List<FlightLuggage> GetFlightLuggage()
        {
            List<FlightLuggage> flightLuggageList = new List<FlightLuggage>();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                flightLuggageList = context.FlightLuggage.ToList();

            }

            return flightLuggageList;
        }


        public bool AddFlightLuggage(FlightLuggage flightLuggage)
        {
            int rowsEffected = -1;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                context.Add(flightLuggage);
                try
                {
                    rowsEffected = context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            return rowsEffected > 0 ? true : false;
        }

        public FlightLuggage GetFlightLuggage(int id)
        {
            FlightLuggage flightLuggage = new FlightLuggage();

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                flightLuggage = context.FlightLuggage.Where(theLuggage => theLuggage.FlightLuggageId == id).SingleOrDefault();
            }

            return flightLuggage;
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


        public bool AddFlight(Flight flight)
        {
            int rowsEffected = 0;

            using (var context = new DataContext(DataContext.ops.dbOptions))
            {
                int airlineId = flight.Airline.AirlineId;
                flight.Airline = new Airline();
                flight.Airline.AirlineId = airlineId;



                context.Destination.Attach(flight.ToDestination);

                context.Destination.Attach(flight.FromDestination);

                context.Airline.Attach(flight.Airline);
                context.Add(flight);
                rowsEffected = context.SaveChanges();
            }

            return rowsEffected > 0 ? true : false;
        }
    }
}
