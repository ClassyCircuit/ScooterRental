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
        Mock<IEndRentHandlerValidator> endRentHandlerValidator;
        Mock<IGetScooterByIdHandler> getScooterByIdHandler;
        Mock<ICostCalculatorService> costCalculatorService;
        EndRentHandler endRentHandler;

        public EndRentTests(Setup.Mocks context) : base(context)
        {
            endRentHandlerValidator = new Mock<IEndRentHandlerValidator>();
            getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            costCalculatorService = new Mock<ICostCalculatorService>();
        }

        [Fact]
        public void Handler_EndsRent()
        {
            // Arrange
            decimal totalCostForRentalPeriod = GetRandom.Decimal(0, 100);

            endRentHandlerValidator.Setup(x => x.Validate(Mocks.Scooters[0])).Verifiable();
            getScooterByIdHandler.Setup(x => x.Handle(Mocks.ExistingScooterId, Mocks.Company.Id)).Returns(Mocks.Scooters[0]).Verifiable();
            costCalculatorService.Setup(x => x.CalculateRentEventCosts(Mocks.RentEvents[0])).Returns(totalCostForRentalPeriod).Verifiable();
            Mocks.CompanyRepository.Setup(x => x.GetActiveRentEventByScooterId(Mocks.Company.Id, Mocks.Scooters[0].Id)).Returns(Mocks.RentEvents[0]);

            endRentHandler = new EndRentHandler(endRentHandlerValidator.Object, getScooterByIdHandler.Object, costCalculatorService.Object, Mocks.CompanyRepository.Object);
            // Act
            endRentHandler.Handle(Mocks.Scooters[0].Id, Mocks.Company.Id);

            // Assert
            endRentHandlerValidator.Verify();
            getScooterByIdHandler.Verify();
            costCalculatorService.Verify();
        }

        [Fact]
        public void Handler_OnSuccess_ReturnsTotalCost()
        {
            var rentedScooter = ScooterBuilder.Default(Mocks.Company).WithIsRented(true).Build();
            var rentEvent = RentEventBuilder.Default(Mocks.Company, rentedScooter).Build();
            decimal totalCostForRentalPeriod = GetRandom.Decimal(0, 100);

            getScooterByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Mocks.Company.Id)).Returns(rentedScooter);
            costCalculatorService.Setup(x => x.CalculateRentEventCosts(rentEvent)).Returns(totalCostForRentalPeriod);
            Mocks.CompanyRepository.Setup(x => x.GetActiveRentEventByScooterId(Mocks.Company.Id, rentedScooter.Id)).Returns(rentEvent);

            endRentHandler = new EndRentHandler(endRentHandlerValidator.Object, getScooterByIdHandler.Object, costCalculatorService.Object, Mocks.CompanyRepository.Object);

            decimal cost = endRentHandler.Handle(rentedScooter.Id, Mocks.Company.Id);

            cost.ShouldBe(totalCostForRentalPeriod);
        }

        [Fact]
        public void Validator_NotRentedScooter_ThrowsException()
        {
            var notRentedScooter = ScooterBuilder.Default(Mocks.Company).WithIsRented(false).Build();
            IEndRentHandlerValidator validator = new EndRentHandlerValidator();

            Action act = () => validator.Validate(notRentedScooter);

            Should.Throw<ScooterIsNotRentedException>(act);
        }

    }
}
