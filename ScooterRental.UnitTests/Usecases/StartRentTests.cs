using Moq;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Usecases.StartRent;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class StartRentTests : TestBase
    {
        public StartRentTests(Context context) : base(context)
        {
        }

        [Fact]
        public void StartRentHandler_StartsRent()
        {
            var scooter = ScooterBuilder.Default().Build();

            var validator = new Mock<IStartRentValidator>();
            validator.Setup(x => x.Validate(scooter.Id)).Returns(scooter).Verifiable();

            StartRentHandler handler = new StartRentHandler(validator.Object, new Mock<IScooterService>().Object, new Mock<IRentEventService>().Object);
            handler.Handle(scooter.Id);

            validator.Verify();
        }

        [Fact]
        public void StartRentValidator_IsRented_ThrowsException()
        {
            var rentedScooter = ScooterBuilder.Default().WithIsRented(true).Build();

            var getByIdHandler = new Mock<IGetScooterByIdHandler>();
            getByIdHandler.Setup(x => x.Handle(rentedScooter.Id)).Returns(rentedScooter);

            StartRentValidator validator = new StartRentValidator(getByIdHandler.Object);

            Action act = () => validator.Validate(rentedScooter.Id);
            Should.Throw<ScooterIsAlreadyBeingRentedException>(act);
        }
    }
}
