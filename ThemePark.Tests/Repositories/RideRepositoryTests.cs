using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThemePark.Infrastructure;
using ThemePark.Models;
using ThemePark.Repositories.Implementations;
using Xunit;

namespace ThemePark.Tests.Repositories
{
    public class RideRepositoryTests
    {
        [Fact]
        public void RideRepository_GetRideByID_Valid_ID()
        {
            // create in Memort Database 
            var options = new DbContextOptionsBuilder<ThemeParkContext>()
                    .UseInMemoryDatabase(databaseName: "ThemeParkDataBase")
                    .Options;

            using (var context = new ThemeParkContext(options))
            {
               

                context.Rides.Add(new Ride
                {
                    ID = new Guid("9c941643-b050-40de-958c-b5e5e6a885af"),
                    Name = "Teacups",
                    Description = "Spinning ride in a big tea-cup ",
                    ThrillFactor = 2,
                    VomitFactor = 1
                });

                context.Rides.Add(new Ride
                {
                    ID = new Guid("e33c520a-3f9f-4256-bbc2-75e7c3bc352d"),
                    Name = "Log Flume ",
                    Description = "Boat ride with plenty of splashes",
                    ThrillFactor = 3,
                    VomitFactor = 2
                });
                context.SaveChanges();
            }


            using (var context = new ThemeParkContext(options))
            {
                RideRepository controller = new RideRepository(context);
                var ride = controller.GetRideByID(new Guid("e33c520a-3f9f-4256-bbc2-75e7c3bc352d"));

                Assert.Equal("e33c520a-3f9f-4256-bbc2-75e7c3bc352d", ride.ID.ToString());
                Assert.Equal("Log Flume ", ride.Name);
                Assert.Equal(3, ride.ThrillFactor);
            }

        }

        [Fact]
        public void RideRepository_GetAllRide()
        {
            // create in Memort Database 
            var options = new DbContextOptionsBuilder<ThemeParkContext>()
                    .UseInMemoryDatabase(databaseName: "ThemeParkDataBase")
                    .Options;

            using (var context = new ThemeParkContext(options))
            {


                context.Rides.Add(new Ride
                {
                    ID = new Guid("9c941643-b050-40de-958c-b5e5e6a885af"),
                    Name = "Teacups",
                    Description = "Spinning ride in a big tea-cup ",
                    ThrillFactor = 2,
                    VomitFactor = 1
                });

                context.Rides.Add(new Ride
                {
                    ID = new Guid("e33c520a-3f9f-4256-bbc2-75e7c3bc352d"),
                    Name = "Log Flume ",
                    Description = "Boat ride with plenty of splashes",
                    ThrillFactor = 3,
                    VomitFactor = 2
                });
                context.SaveChanges();
            }


            using (var context = new ThemeParkContext(options))
            {
                RideRepository controller = new RideRepository(context);
                var rides = controller.GetAllRides();
     
                Assert.Equal(2, rides.Count);
            }

        }
    }
}
