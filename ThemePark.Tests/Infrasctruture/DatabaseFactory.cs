using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemePark.Infrastructure;
using ThemePark.Models;

namespace ThemePark.Tests.Infrasctruture
{
    public class DatabaseFactory
    {
        public static ThemeParkContext CreateDbContext()
        {
            // create in Memort Database 
            var options = new DbContextOptionsBuilder<ThemeParkContext>()
                    .UseInMemoryDatabase(databaseName: "ThemeParkDataBase")
                    .Options;

            var dbContext = new ThemeParkContext(options);


            using (var context = new ThemeParkContext(options))
            {
                if (!context.Rides.Any())
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
            }

            return dbContext;
        }


    }
}
