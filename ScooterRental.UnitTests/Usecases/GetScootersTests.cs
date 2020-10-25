using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
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
        public GetScootersTests(Mocks context) : base(context)
        {
        }

        [Fact]
        public void GetAllScooters_ReturnsListOfScooters()
        {
            // Arrange
            GetScootersHandler handler = new GetScootersHandler(Mocks.CompanyRepository.Object);

            // Act
            IList<Scooter> result = handler.Handle(Mocks.Company.Id);

            // Assert
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public void GetScooterById_ReturnsOneScooter()
        {
            // Arrange
            GetScooterByIdHandler handler = new GetScooterByIdHandler(Mocks.CompanyRepository.Object, Mocks.GetScooterByIdValidator.Object);

            // Act
            Scooter result = handler.Handle(Mocks.ExistingScooterId, Mocks.Company.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Mocks.ExistingScooterId);
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
