using Moq;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Usecases.RemoveScooter;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class RemoveScooterTests : TestBase
    {
        public Mock<RemoveScooterValidator> RemoveScooterValidator;

        public RemoveScooterTests(Context context) : base(context)
        {
            RemoveScooterValidator = new Mock<RemoveScooterValidator>(Context.ScooterService.Object);
        }

        [Fact]
        public void RemoveScooterValidator_IsRented_ThrowsException()
        {
            // Arrange
            var rentedScooter = ScooterBuilder.Default().WithIsRented(true).Build();

            var getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            getScooterByIdHandler.Setup(x => x.Handle(rentedScooter.Id)).Returns(rentedScooter);

            RemoveScooterValidator validator = new RemoveScooterValidator(getScooterByIdHandler.Object);

            // Act
            Action act = () => validator.Validate(rentedScooter.Id);

            // Assert
            Should.Throw<RentedScooterCannotBeRemovedException>(act);
        }

        [Fact]
        public void RemoveScooter_RemovesScooter()
        {
            string id = "1";

            var service = new Mock<IScooterService>();
            service.Setup(x=>x.RemoveScooter(id)).Verifiable();

            var validator = new Mock<IRemoveScooterValidator>();
            validator.Setup(x=>x.Validate(id)).Verifiable();

            RemoveScooterHandler handler = new RemoveScooterHandler(service.Object, validator.Object);

            handler.Handle(id);

            validator.Verify();
            service.Verify();
        }
    }
}
