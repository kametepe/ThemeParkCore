using System;
using System.Collections.Generic;
using System.Linq; 
using ThemePark.Models;

namespace ThemePark.Infrastructure
{
    public static class GoldebVaultSeedData
    {
        public static void EnsureSeedData(this ThemeParkContext db)
        {
            
            if (!db.Rides.Any())
            {
                Ride ride = new Ride { ID = new Guid("1dd6c4dd-5041-4637-a482-39c1530ec047"), Name = "RollerCoster", Description = "Train ride that speeds you arlong", ThrillFactor = 5, VomitFactor = 3 };
                db.Rides.Add(ride);
                
                ride = new Ride { ID = new Guid("76d020c8-f845-412a-93ae-4f18b136dad6"), Name = "Log Flume ", Description = "Boat ride with plenty of splashes", ThrillFactor = 3, VomitFactor = 2 };
                db.Rides.Add(ride);

                ride = new Ride { ID = new Guid("3f258a96-c513-4ef2-9a8e-85424b601862"), Name = "Teacups", Description = "Spinning ride in a big tea-cup ", ThrillFactor = 2, VomitFactor = 1 };
                db.Rides.Add(ride);

                ride = new Ride { ID = new Guid("7044ad69-4af5-4683-876d-0d07fdd87fdd"), Name = "Bumper cars", Description = "Bumper cars or dodgems is the generic name for a type of flat ride consisting of several small electrically powered cars which draw power from the floor and/or ceiling ", ThrillFactor = 2, VomitFactor = 0 };
                db.Rides.Add(ride);

                db.SaveChanges();
            }
  
        }
    }
}
