using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ThemePark.Infrastructure
{
    public class ThemeParkContextFactory : IDesignTimeDbContextFactory<ThemeParkContext>
    {
        public IConfiguration _Configuration { get; }


        public ThemeParkContextFactory(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public ThemeParkContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ThemeParkContext>();
            string connection = _Configuration.GetConnectionString("ThemeParkConnection");
            optionsBuilder.UseSqlServer(connection);
            // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ThemePark;Trusted_Connection=True;ConnectRetryCount=0");

            return new ThemeParkContext(optionsBuilder.Options);
        }
    }
}
