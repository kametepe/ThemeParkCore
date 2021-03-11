using System;
using Moq;
using ThemePark.Controllers;
using ThemePark.Models;
using ThemePark.Services.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace ThemePark.Tests
{
    
    public class RideControllerTest
    {
        [Fact]
        public void GetReturnsRideWithSameId()
        {
            string guid = "80b8aa6b-4dde-4df5-9a40-60d2cd4d0e04";
            // Arrange
            var mockRideService = new Mock<IRideService>();
            mockRideService.Setup(x => x.GetRideByID(Guid.Parse(guid)))
                .Returns(new Ride { ID = Guid.Parse(guid) });

            var controller = new RideController(mockRideService.Object);

            // Act
            var contentResult = controller.GetRideByID(Guid.Parse(guid));
            

            // Assert            
            Assert.NotNull(contentResult);
            Assert.Equal(Guid.Parse(guid), contentResult.ID);
        }


        [Fact]
        public void GetAllRidess_ShouldReturnAllRides()
        {             
            // Arrange
            var mockRideService = new Mock<IRideService>();
            mockRideService.Setup(x => x.GetAllRides())
                .Returns(GetTestRides());

            var controller = new RideController(mockRideService.Object);



        // Act
        var contentResult = controller.GetAllRides();
        
        // Assert
            Assert.NotNull(contentResult);
         Assert.Equal(GetTestRides().Count, contentResult.Count);
        }

    private List<Ride> GetTestRides()
    {
        var testRides = new List<Ride>();
        testRides.Add(new Ride { ID = Guid.Parse("f822bf48-fa37-4c25-b925-5d0ba28e07c6"), Name = "Demo1" });
        testRides.Add(new Ride { ID = Guid.Parse("43fab181-d9be-4e4e-ad4e-f7628e4f018a"), Name = "Demo2" });
        testRides.Add(new Ride { ID = Guid.Parse("0e228228-780c-4486-98f9-9817e6486cc2"), Name = "Demo3" });
        testRides.Add(new Ride { ID = Guid.Parse("5cc0d287-ccd7-489b-baea-c9068b1c71d4"), Name = "Demo4" });

        return testRides;
    }

}

}
