using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThemePark.Domains;
using ThemePark.Exceptions;
using ThemePark.Infrastructure;
using ThemePark.Models;
using ThemePark.Repositories.Interfaces;

namespace ThemePark.Repositories.Implementations
{
    public class RideRepository : IRideRepository
    {
        private ThemeParkContext _context;
        
        public RideRepository(ThemeParkContext context)
        { 
            _context = context;            
        }

        public List<Ride> GetAllRides()
        {
            return _context.Rides.ToList<Ride>();
        }

        public Ride GetRideByID(Guid id)
        {
            Ride ride = _context.Rides.Where(r => r.ID == id).FirstOrDefault();

            if(ride == null)
            {
                throw new NotFoundException(id.ToString());
            }

            return ride;
        }

        public List<Ride> SearchRide(RideParam param)
        {
            return _context.Rides.Where(r => r.ThrillFactor >= param.MinimumThrillFactor).ToList<Ride>();
        }
 
    }
}