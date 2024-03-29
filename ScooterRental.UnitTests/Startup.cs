﻿using Microsoft.Extensions.DependencyInjection;
using ScooterRental.Core.Entities;
using ScooterRental.Infrastructure.Data;
using ScooterRental.UnitTests.Setup;
using System.Collections.Generic;

namespace ScooterRental.UnitTests
{
    /// <summary>
    /// Setup dependency injection container for unit tests.
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Scooter, Scooter>();
            services.AddTransient<Data, Data>();
            services.AddScoped<IList<Scooter>, List<Scooter>>();
            services.AddScoped<IList<Company>, List<Company>>();

            Context context = new Context(new List<Company>());
            //services.AddScoped<Context, context>();
        }
    }
}
