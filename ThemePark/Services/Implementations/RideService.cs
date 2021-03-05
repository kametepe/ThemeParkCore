using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThemePark.Domains;
using ThemePark.Models;
using ThemePark.Repositories.Interfaces;
using ThemePark.Services.Interfaces;

namespace ThemePark.Services.Implementations
{
    
    public class RideService : IRideService
    {
        private IRideRepository rideRepository;

        public RideService(IRideRepository rideRepo)
        {
            rideRepository = rideRepo;
        }
        public List<Ride> GetAllRides()
        {
            return rideRepository.GetAllRides();
        }

        public Ride GetRideByID(Guid id)
        {
            return rideRepository.GetRideByID(id);
        }

        public List<Ride> SearchRide(RideParam param)
        {
            return rideRepository.SearchRide(param);
        }
    }
}