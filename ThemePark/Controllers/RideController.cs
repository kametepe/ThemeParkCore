using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ThemePark.Models;
using ThemePark.Services.Interfaces;

namespace ThemePark.Controllers
{    
    [Route("api/Ride")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService rideService;
        public RideController(IRideService rideSvc)
        {
            rideService = rideSvc; 
        }


        [HttpGet("rides")]
        public IEnumerable<Ride> GetAllRides()
        {
            return rideService.GetAllRides();
        }

        [HttpGet("{id}")]
        public Ride GetRideByID(Guid id)
        {
            return rideService.GetRideByID(id);
        }

    }
}
