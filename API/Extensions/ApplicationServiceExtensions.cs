using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Adds a scoped service of the type specified in ITokenService with an implementation type 
            // specified in TokenService to the specified IServiceCollection
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();

            // for AutoMapper to go ahead and find those profiles the CreateMaps
            // that we created inside class AutoMapperProfiles
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // Configures the context to connect to a SQLite database.
            // appsettings.Development.json has defined: "ConnectionStrings":"DefaultConnection": "Data source=datingapp.db"
            // here we add this connection of DB to DbContext instance DataContext
            services.AddDbContext<DataContext>(options =>{
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}