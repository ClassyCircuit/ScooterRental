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
    public class RemoveScooterTests : TestBase
    {
        public Mock<RemoveScooterValidator> RemoveScooterValidator;
        private Mock<IScooterRepository> ScooterRepository;

        public RemoveScooterTests(Data context) : base(context)
        {
            RemoveScooterValidator = new Mock<RemoveScooterValidator>();
            ScooterRepository = new Mock<IScooterRepository>();
        }

        [Fact]
        public void RemoveScooterValidator_IsRented_ThrowsException()
        {
            // Arrange
            var rentedScooter = ScooterBuilder.Default(Data.Company).WithIsRented(true).Build();

            var getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            getScooterByIdHandler.Setup(x => x.Handle(rentedScooter.Id, Data.Company.Id)).Returns(rentedScooter);

            RemoveScooterValidator validator = new RemoveScooterValidator(getScooterByIdHandler.Object);

            // Act
            Action act = () => validator.Validate(rentedScooter.Id, Data.Company.Id);

            // Assert
            Should.Throw<RentedScooterCannotBeRemovedException>(act);
        }

        [Fact]
        public void RemoveScooter_RemovesScooter()
        {
            string id = "1";

            var validator = new Mock<IRemoveScooterValidator>();
            validator.Setup(x => x.Validate(id, Data.Company.Id)).Verifiable();

            RemoveScooterHandler handler = new RemoveScooterHandler(ScooterRepository.Object, validator.Object);

            handler.Handle(id, Data.Company.Id);

            validator.Verify();
            validator.Verify();
        }
    }
}
