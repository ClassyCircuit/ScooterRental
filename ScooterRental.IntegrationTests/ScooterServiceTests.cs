using ScooterRental.Core.Interfaces.Services;
using Xunit;

namespace ScooterRental.IntegrationTests
{
    public class ScooterServiceTests
    {
        private readonly IScooterService scooterService;

        public ScooterServiceTests(IScooterService scooterService)
        {
            this.scooterService = scooterService;
        }

        [Fact]
        public void CanAddScooter()
        {
            //scooterService.AddScooter()
        }
    }
}
