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
    public class RemoveScooterTests : TestBase
    {
        public Mock<RemoveScooterValidator> RemoveScooterValidator;

        public RemoveScooterTests(Setup.Mocks context) : base(context)
        {
            RemoveScooterValidator = new Mock<RemoveScooterValidator>();
        }

        [Fact]
        public void RemoveScooterValidator_IsRented_ThrowsException()
        {
            // Arrange
            var rentedScooter = ScooterBuilder.Default(Mocks.Company).WithIsRented(true).Build();

            var getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            getScooterByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Mocks.Company.Id)).Returns(rentedScooter);

            RemoveScooterValidator validator = new RemoveScooterValidator(getScooterByIdHandler.Object);

            // Act
            Action act = () => validator.Validate(rentedScooter.Id, Mocks.Company.Id);

            // Assert
            Should.Throw<RentedScooterCannotBeRemovedException>(act);
        }

        [Fact]
        public void RemoveScooter_RemovesScooter()
        {
            string id = "1";

            var validator = new Mock<IRemoveScooterValidator>();
            validator.Setup(x => x.Validate(id, Mocks.Company.Id)).Verifiable();

            RemoveScooterHandler handler = new RemoveScooterHandler(Mocks.CompanyRepository.Object, validator.Object);

            handler.Handle(id, Mocks.Company.Id);

            validator.Verify();
            validator.Verify();
        }
    }
}
