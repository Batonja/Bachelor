using Common.Models;
using Common.Models.Airline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AirlineDestination>().HasKey(ad => new { ad.AirlineId, ad.DestinationId });
            modelBuilder.Entity<AirlineFlightLuggage>().HasKey(afl => new { afl.AirlineId, afl.FlightLuggageId });
            modelBuilder.Entity<Airline>().HasMany(airline => airline.AvailableFlightLuggage).WithOne(afl => afl.Airline).HasForeignKey(afl => afl.AirlineId);



            //modelBuilder.Ignore<AppSettings>();


        }


        public class OptionsBuild
        {
            public OptionsBuild()
            {
                settings = new AppConfiguration();
                optionsBuilder = new DbContextOptionsBuilder<DataContext>();
                optionsBuilder.UseSqlServer(settings.sqlConnectionString);
                dbOptions = optionsBuilder.Options;
            }

            public DbContextOptionsBuilder optionsBuilder { get; set; }
            public DbContextOptions dbOptions { get; set; }
            private AppConfiguration settings { get; set; }
        }

        public static OptionsBuild ops = new OptionsBuild();
        
        public DbSet<AirlineFlightLuggage> AirlineFlightLuggage { get; set; }
        public DbSet<AirlineDestination> AirlineDestination { get; set; }
        public DbSet<Airline> Airline { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<FlightLuggage> FlightLuggage { get; set; }
    }
}

