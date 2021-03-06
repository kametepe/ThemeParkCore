using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemePark.Domains;
using ThemePark.Models;
using ThemePark.Repositories.Interfaces;
using ThemePark.Services.Implementations;
using Xunit;

namespace ThemePark.Tests.Services
{
    public class RideServiceTest
    {
        [Fact]
        public void GetReturnsRideWithSameId()
        {
            string guid = "80b8aa6b-4dde-4df5-9a40-60d2cd4d0e04";
            // Arrange
            var mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(x => x.GetRideByID(Guid.Parse(guid)))
                .Returns(new Ride { ID = Guid.Parse(guid) });

            var controller = new RideService(mockRideRepository.Object);

            // Act
            var contentResult = controller.GetRideByID(Guid.Parse(guid));


            // Assert            
            Assert.NotNull(contentResult);
            Assert.Equal(Guid.Parse(guid), contentResult.ID);
        }

        [Fact]
        public void GetAllRides_ShouldReturnAllRides()
        {
            // Arrange
            var mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(x => x.GetAllRides())
                .Returns(GetTestRides());

            var controller = new RideService(mockRideRepository.Object);



            // Act
            var contentResult = controller.GetAllRides();

            // Assert
            Assert.NotNull(contentResult);
            Assert.Equal(GetTestRides().Count, contentResult.Count);
        }


        [Fact]
        public void SearchRide_ShouldReturnMatchedRides()
        {


            RideParam searchparam = new RideParam { MinimumThrillFactor = 2 };

            // Arrange
            var mockRideRepository = new Mock<IRideRepository>();
            mockRideRepository.Setup(x => x.SearchRide(searchparam))
                .Returns(GetTestRides().Where(r => r.ThrillFactor >= searchparam.MinimumThrillFactor).ToList<Ride>());

            var controller = new RideService(mockRideRepository.Object);

            

            // Act
            var contentResult = controller.SearchRide(searchparam);

            // Assert
            Assert.NotNull(contentResult);
            Assert.Equal(2, contentResult.Count);
        }

        private List<Ride> GetTestRides()
        {
            var testRides = new List<Ride>();
            testRides.Add(new Ride { ID = Guid.Parse("f822bf48-fa37-4c25-b925-5d0ba28e07c6"), Name = "Demo1" , ThrillFactor = 1});
            testRides.Add(new Ride { ID = Guid.Parse("43fab181-d9be-4e4e-ad4e-f7628e4f018a"), Name = "Demo2", ThrillFactor = 4 });
            testRides.Add(new Ride { ID = Guid.Parse("0e228228-780c-4486-98f9-9817e6486cc2"), Name = "Demo3", ThrillFactor = 5 });
            

            return testRides;
        }
    }
}
