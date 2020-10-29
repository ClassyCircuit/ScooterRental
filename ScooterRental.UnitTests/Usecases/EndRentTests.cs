using Moq;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Services.Builders;
using ScooterRental.Core.Usecases;
using ScooterRental.Core.Validators;
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
        private Mock<IScooterRepository> scooterRepository;
        private Mock<IRentEventRepository> rentEventRepository;
        private Mock<IBusinessLogicRepository> businessLogicRepository;

        public EndRentTests(Data context) : base(context)
        {
            endRentValidator = new Mock<IEndRentValidator>();
            getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            rentalCostService = new Mock<IRentalCostService>();
            rentEventUpdateHandler = new Mock<IRentEventUpdateHandler>();
            scooterRepository = new Mock<IScooterRepository>();
            rentEventRepository = new Mock<IRentEventRepository>();
            businessLogicRepository = new Mock<IBusinessLogicRepository>();

            businessLogicRepository.Setup(x => x.GetPriceLimits(Data.Company.Id)).Returns(Data.PriceLimit);
        }

        [Fact]
        public void Handler_DelegatesToComponents()
        {
            // Arrange
            decimal totalCostForRentalPeriod = GetRandom.Decimal(0, 100);
            DateTime endDate = DateTime.UtcNow;

            endRentValidator.Setup(x => x.Validate(Data.Scooters[0])).Verifiable();
            getScooterByIdHandler.Setup(x => x.Handle(Data.ExistingScooterId, Data.Company.Id)).Returns(Data.Scooters[0]).Verifiable();
            rentalCostService.Setup(x => x.Calculate(Data.RentEvents[0], endDate, Data.PriceLimit)).Returns(Data.RentEvents).Verifiable();
            rentEventRepository.Setup(x => x.GetActiveRentEventByScooterId(Data.Company.Id, Data.Scooters[0].Id)).Returns(Data.RentEvents[0]);
            rentEventUpdateHandler.Setup(x => x.Handle(Data.Company.Id, Data.RentEvents)).Verifiable();

            endRentHandler = new EndRentHandler(endRentValidator.Object, getScooterByIdHandler.Object, rentalCostService.Object, rentEventRepository.Object, rentEventUpdateHandler.Object, scooterRepository.Object, businessLogicRepository.Object);

            // Act
            endRentHandler.Handle(Data.Scooters[0].Id, Data.Company.Id, endDate);

            // Assert
            endRentValidator.Verify();
            getScooterByIdHandler.Verify();
            rentalCostService.Verify();
            rentEventUpdateHandler.Verify();
        }

        [Fact]
        public void Handler_OnSuccess_ReturnsTotalCost()
        {
            var rentedScooter = ScooterBuilder.Default(Data.Company).WithIsRented(true).Build();
            var rentEvent = RentEventBuilder.Default(Data.Company, rentedScooter).Build();
            var endDate = DateTime.UtcNow;
            decimal totalCostForRentalPeriod = CalculateExpectedRentalCosts();

            getScooterByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Data.Company.Id)).Returns(rentedScooter);
            rentalCostService.Setup(x => x.Calculate(rentEvent, endDate, Data.PriceLimit)).Returns(Data.RentEvents);
            rentEventRepository.Setup(x => x.GetActiveRentEventByScooterId(Data.Company.Id, rentedScooter.Id)).Returns(rentEvent);

            endRentHandler = new EndRentHandler(endRentValidator.Object, getScooterByIdHandler.Object, rentalCostService.Object, rentEventRepository.Object, rentEventUpdateHandler.Object, scooterRepository.Object, businessLogicRepository.Object);

            decimal cost = endRentHandler.Handle(rentedScooter.Id, Data.Company.Id, endDate);

            cost.ShouldBe(totalCostForRentalPeriod);
        }

        private decimal CalculateExpectedRentalCosts()
        {
            decimal cost = 0;
            foreach (var x in Data.RentEvents)
            {
                cost += x.TotalPrice;
            }
            return cost;
        }

        [Fact]
        public void Validator_NotRentedScooter_ThrowsException()
        {
            var notRentedScooter = ScooterBuilder.Default(Data.Company).WithIsRented(false).Build();
            IEndRentValidator validator = new EndRentValidator();

            Action act = () => validator.Validate(notRentedScooter);

            Should.Throw<ScooterIsNotRentedException>(act);
        }

    }
}
