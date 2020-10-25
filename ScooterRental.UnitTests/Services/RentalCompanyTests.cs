using Moq;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Services;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class RentalCompanyTests : TestBase
    {
        public RentalCompanyTests(Setup.Mocks context) : base(context)
        {
        }

        [Fact]
        public void StartRent_DelegatesToRentHandler()
        {
            string id = GetRandom.UniqueId();

            var handler = new Mock<IStartRentHandler>();
            handler.Setup(x => x.Handle(id, Mocks.Company)).Verifiable();

            IRentalCompany company = new RentalCompany(handler.Object, Mocks.Company, new Mock<IEndRentHandler>().Object, new Mock<ICalculateIncomeHandler>().Object);

            company.StartRent(id);
            handler.Verify();
        }

    }
}
