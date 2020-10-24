using Microsoft.Extensions.DependencyInjection;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Infrastructure;

namespace ScooterRental.IntegrationTests
{
    /// <summary>
    /// Setup dependency injection container for integration tests.
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IScooterService, ScooterService>();
        }
    }
}
