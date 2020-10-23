using Microsoft.Extensions.DependencyInjection;
using Moq;
using ScooterRental.Core.Interfaces;
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
            //services.AddTransient<IScooterService, Mock<IScooterService>>();
            services.AddTransient<MockObjects, MockObjects>();
        }
    }
}
