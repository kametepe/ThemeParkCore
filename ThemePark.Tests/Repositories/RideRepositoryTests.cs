using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemePark.Domains;
using ThemePark.Exceptions;
using ThemePark.Infrastructure;
using ThemePark.Models;
using ThemePark.Repositories.Implementations;
using ThemePark.Tests.Infrasctruture;
using Xunit;

namespace ThemePark.Tests.Repositories
{
    public class RideRepositoryTests
    {
        [Fact]
        public void RideRepository_GetRideByID_Valid_ID()
        {
           using (var context = DatabaseFactory.CreateDbContext())
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
          
            using (var context = DatabaseFactory.CreateDbContext())
            {
                RideRepository controller = new RideRepository(context);
                var rides = controller.GetAllRides();
     
                Assert.Equal(2, rides.Count);
            }

        }


        [Fact]
        public void RideRepository_GetSearchRide()
        {

            using (var context = DatabaseFactory.CreateDbContext())
            {
                RideParam searchparam = new RideParam { MinimumThrillFactor = 1 };

                RideRepository controller = new RideRepository(context);
                var rides = controller.SearchRide(searchparam);

                Assert.Equal(2, rides.Count);
            }

        }


        [Fact]
        public void RideRepository_ThrowNotFoundException()
        {

            using (var context = DatabaseFactory.CreateDbContext())
            {
                

                RideRepository controller = new RideRepository(context);
                 
                Assert.Throws<NotFoundException>(() => controller.GetRideByID(new Guid("d33c520a-3f9f-4256-bbc2-75e7c3bc352d")));
            }

        }


    }
}
