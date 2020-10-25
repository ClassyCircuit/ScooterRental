using Moq;
using ScooterRental.Core.Exceptions;
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
    public class StartRentTests : TestBase
    {
        public StartRentTests(Setup.Mocks context) : base(context)
        {
        }

        [Fact]
        public void StartRentHandler_StartsRent()
        {
            var scooter = ScooterBuilder.Default(Mocks.Company).Build();

            var validator = new Mock<IStartRentValidator>();
            validator.Setup(x => x.Validate(scooter.Id, Mocks.Company.Id)).Returns(scooter).Verifiable();

            StartRentHandler handler = new StartRentHandler(validator.Object, Mocks.CompanyRepository.Object);
            handler.Handle(scooter.Id, Mocks.Company);

            validator.Verify();
        }

        [Fact]
        public void StartRentValidator_IsRented_ThrowsException()
        {
            var rentedScooter = ScooterBuilder.Default(Mocks.Company).WithIsRented(true).Build();

            var getByIdHandler = new Mock<IGetScooterByIdHandler>();
            getByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Mocks.Company.Id)).Returns(rentedScooter);

            StartRentValidator validator = new StartRentValidator(getByIdHandler.Object);

            Action act = () => validator.Validate(rentedScooter.Id, Mocks.Company.Id);
            Should.Throw<ScooterIsAlreadyBeingRentedException>(act);
        }
    }
}
