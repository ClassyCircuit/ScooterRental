using ScooterRental.Core.Interfaces.Usecases;
using Xunit;

namespace ScooterRental.IntegrationTests
{
    public class ScooterServiceTests
    {
        private readonly IGetScooterServiceHandler scooterServiceHandler;
        private readonly IGetRentalCompanyHandler rentalCompanyHandler;

        public ScooterServiceTests(IGetScooterServiceHandler scooterServiceHandler, IGetRentalCompanyHandler rentalCompanyHandler)
        {
            this.scooterServiceHandler = scooterServiceHandler;
            this.rentalCompanyHandler = rentalCompanyHandler;
        }

        [Fact]
        public void CanAddScooter()
        {
            //scooterService.AddScooter()
        }
    }
}
