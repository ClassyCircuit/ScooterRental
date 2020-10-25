using Microsoft.Extensions.DependencyInjection;
using ScooterRental.Core.Entities;
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
            services.AddTransient<Mocks, Mocks>();
            services.AddScoped<IList<Scooter>, List<Scooter>>();
        }
    }
}
