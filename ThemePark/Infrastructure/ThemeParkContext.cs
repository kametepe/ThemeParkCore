using Microsoft.EntityFrameworkCore;
using ThemePark.Models;

namespace ThemePark.Infrastructure
{
    public class ThemeParkContext : DbContext
    {
        public ThemeParkContext(DbContextOptions<ThemeParkContext> options)
            : base(options)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                Database.Migrate();
            }
        }
                 
        public DbSet<Ride> Rides { get; set; }                
        
        
    }
}
