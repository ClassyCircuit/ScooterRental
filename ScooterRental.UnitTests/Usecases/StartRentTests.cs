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
    public class StartRentTests : TestBase
    {
        private Mock<IScooterRepository> ScooterRepository;
        private Mock<IRentEventRepository> RentEventRepository;

        public StartRentTests(Setup.Data context) : base(context)
        {
            ScooterRepository = new Mock<IScooterRepository>();
            RentEventRepository = new Mock<IRentEventRepository>();
        }

        [Fact]
        public void StartRentHandler_StartsRent()
        {
            var scooter = ScooterBuilder.Default(Data.Company).Build();

            var validator = new Mock<IStartRentValidator>();
            validator.Setup(x => x.Validate(scooter.Id, Data.Company.Id)).Returns(scooter).Verifiable();

            StartRentHandler handler = new StartRentHandler(validator.Object, RentEventRepository.Object, ScooterRepository.Object);
            handler.Handle(scooter.Id, Data.Company);

            validator.Verify();
        }

        [Fact]
        public void StartRentValidator_IsRented_ThrowsException()
        {
            var rentedScooter = ScooterBuilder.Default(Data.Company).WithIsRented(true).Build();

            var getByIdHandler = new Mock<IGetScooterByIdHandler>();
            getByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Data.Company.Id)).Returns(rentedScooter);

            StartRentValidator validator = new StartRentValidator(getByIdHandler.Object);

            Action act = () => validator.Validate(rentedScooter.Id, Data.Company.Id);
            Should.Throw<ScooterIsAlreadyBeingRentedException>(act);
        }
    }
}
