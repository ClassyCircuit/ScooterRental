using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Usecases;
using ScooterRental.Core.Validators;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class GetScootersTests : TestBase
    {
        Mock<GetScooterByIdValidator> GetScooterByIdValidator;
        Mock<IScooterRepository> ScooterRepository;

        public GetScootersTests(Data context) : base(context)
        {
            GetScooterByIdValidator = new Mock<GetScooterByIdValidator>();
            ScooterRepository = new Mock<IScooterRepository>();
            ScooterRepository.Setup(x => x.GetScooters(Data.Company.Id)).Returns(Data.Scooters);
            ScooterRepository.Setup(x => x.GetScooterById(Data.Company.Id, Data.ExistingScooterId)).Returns(Data.Scooters[0]);
        }

        [Fact]
        public void GetAllScooters_ReturnsListOfScooters()
        {
            // Arrange
            GetScootersHandler handler = new GetScootersHandler(ScooterRepository.Object);

            // Act
            IList<Scooter> result = handler.Handle(Data.Company.Id);

            // Assert
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public void GetScooterById_ReturnsOneScooter()
        {
            // Arrange
            GetScooterByIdHandler handler = new GetScooterByIdHandler(ScooterRepository.Object, GetScooterByIdValidator.Object);

            // Act
            Scooter result = handler.Handle(Data.ExistingScooterId, Data.Company.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Data.ExistingScooterId);
        }

        [Fact]
        public void GetScooterById_InvalidId_ThrowsException()
        {
            // Arrange
            GetScooterByIdValidator validator = new GetScooterByIdValidator();

            // Act
            Action act = () => validator.Validate("");

            // Assert
            Should.Throw<IdCannotBeEmptyException>(act);
        }
    }
}
