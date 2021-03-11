using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using ThemePark.Infrastructure;
using ThemePark.Repositories.Implementations;
using ThemePark.Repositories.Interfaces;
using ThemePark.Services.Implementations;
using ThemePark.Services.Interfaces;

namespace ThemePark
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("ThemeParkConnection");
            services.AddDbContext<ThemeParkContext>
                (options => options.UseSqlServer(connection));

            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IRideRepository, RideRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ThemePark API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService ="None",
                    Contact = new Contact
                    {
                        Name = "Koudjo AMETEPE",
                        Email = string.Empty,
                        Url = "https://twitter.com/kametepe",
                    }
                    
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
