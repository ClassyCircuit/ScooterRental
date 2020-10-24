using Microsoft.Extensions.DependencyInjection;
using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using ScooterRental.UnitTests.Setup;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddTransient<Context, Context>();
            services.AddScoped<IList<Scooter>, List<Scooter>>();
        }
    }
}
