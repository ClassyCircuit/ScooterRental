using Moq;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Usecases;
using ScooterRental.Core.Validators;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class EndRentTests : TestBase
    {
        private Mock<IEndRentValidator> endRentValidator;
        private Mock<IGetScooterByIdHandler> getScooterByIdHandler;
        private Mock<IRentalCostService> rentalCostService;
        private Mock<IRentEventUpdateHandler> rentEventUpdateHandler;
        private EndRentHandler endRentHandler;

        public EndRentTests(Setup.Mocks context) : base(context)
        {
            endRentValidator = new Mock<IEndRentValidator>();
            getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            rentalCostService = new Mock<IRentalCostService>();
            rentEventUpdateHandler = new Mock<IRentEventUpdateHandler>();
        }

        [Fact]
        public void Handler_DelegatesToComponents()
        {
            // Arrange
            decimal totalCostForRentalPeriod = GetRandom.Decimal(0, 100);
            DateTime endDate = DateTime.UtcNow;

            endRentValidator.Setup(x => x.Validate(Mocks.Scooters[0])).Verifiable();
            getScooterByIdHandler.Setup(x => x.Handle(Mocks.ExistingScooterId, Mocks.Company.Id)).Returns(Mocks.Scooters[0]).Verifiable();
            rentalCostService.Setup(x => x.Calculate(Mocks.RentEvents[0], endDate)).Returns(Mocks.RentEvents).Verifiable();
            Mocks.CompanyRepository.Setup(x => x.GetActiveRentEventByScooterId(Mocks.Company.Id, Mocks.Scooters[0].Id)).Returns(Mocks.RentEvents[0]);

            endRentHandler = new EndRentHandler(endRentValidator.Object, getScooterByIdHandler.Object, rentalCostService.Object, Mocks.CompanyRepository.Object, rentEventUpdateHandler.Object);
            rentEventUpdateHandler.Setup(x => x.Handle(Mocks.Company.Id, Mocks.RentEvents)).Verifiable();

            // Act
            endRentHandler.Handle(Mocks.Scooters[0].Id, Mocks.Company.Id, endDate);

            // Assert
            endRentValidator.Verify();
            getScooterByIdHandler.Verify();
            rentalCostService.Verify();
            rentEventUpdateHandler.Verify();
        }

        [Fact]
        public void Handler_OnSuccess_ReturnsTotalCost()
        {
            var rentedScooter = ScooterBuilder.Default(Mocks.Company).WithIsRented(true).Build();
            var rentEvent = RentEventBuilder.Default(Mocks.Company, rentedScooter).Build();
            var endDate = DateTime.UtcNow;
            decimal totalCostForRentalPeriod = CalculateExpectedRentalCosts();

            getScooterByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Mocks.Company.Id)).Returns(rentedScooter);
            rentalCostService.Setup(x => x.Calculate(rentEvent, endDate)).Returns(Mocks.RentEvents);
            Mocks.CompanyRepository.Setup(x => x.GetActiveRentEventByScooterId(Mocks.Company.Id, rentedScooter.Id)).Returns(rentEvent);

            endRentHandler = new EndRentHandler(endRentValidator.Object, getScooterByIdHandler.Object, rentalCostService.Object, Mocks.CompanyRepository.Object, new Mock<IRentEventUpdateHandler>().Object);

            decimal cost = endRentHandler.Handle(rentedScooter.Id, Mocks.Company.Id, endDate);

            cost.ShouldBe(totalCostForRentalPeriod);
        }

        private decimal CalculateExpectedRentalCosts()
        {
            decimal cost = 0;
            foreach (var x in Mocks.RentEvents)
            {
                cost += x.TotalPrice;
            }
            return cost;
        }

        [Fact]
        public void Validator_NotRentedScooter_ThrowsException()
        {
            var notRentedScooter = ScooterBuilder.Default(Mocks.Company).WithIsRented(false).Build();
            IEndRentValidator validator = new EndRentValidator();

            Action act = () => validator.Validate(notRentedScooter);

            Should.Throw<ScooterIsNotRentedException>(act);
        }

    }
}
