using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThemePark.Domains;
using ThemePark.Exceptions;
using ThemePark.Models;
using ThemePark.Repositories.Interfaces;

namespace ThemePark.Repositories.Implementations
{
    public class RideRepository : IRideRepository
    {

        public List<Ride> GetAllRides()
        {
            return GetStaticRides();
        }

        public Ride GetRideByID(Guid id)
        {
            Ride ride = GetStaticRides().Where(r => r.ID == id).FirstOrDefault();

            if(ride == null)
            {
                throw new NotFoundException(id.ToString());
            }

            return ride;
        }

        public List<Ride> SearchRide(RideParam param)
        {
            throw new NotImplementedException();
        }

        private List<Ride> GetStaticRides()
        {
            var rides = new List<Ride>();
            rides.Add(new Ride { ID = new Guid("1dd6c4dd-5041-4637-a482-39c1530ec047"), Name = "RollerCoster", Description ="Train ride that speeds you arlong", ThrillFactor = 5, VomitFactor =3 });
            rides.Add(new Ride { ID = new Guid("76d020c8-f845-412a-93ae-4f18b136dad6"), Name = "Log Flume ", Description= "Boat ride with plenty of splashes", ThrillFactor = 3, VomitFactor = 2 });
            rides.Add(new Ride { ID = new Guid("3f258a96-c513-4ef2-9a8e-85424b601862"), Name = "Teacups", Description ="Spinning ride in a big tea-cup ",  ThrillFactor = 2, VomitFactor = 1 });
            rides.Add(new Ride { ID  = new Guid("7044ad69-4af5-4683-876d-0d07fdd87fdd"), Name = "Bumper cars", Description = "Bumper cars or dodgems is the generic name for a type of flat ride consisting of several small electrically powered cars which draw power from the floor and/or ceiling ", ThrillFactor = 2, VomitFactor = 0 });

            return rides;
        }
    }
}