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
        public GetScootersTests(Data context) : base(context)
        {
        }

        [Fact]
        public void GetAllScooters_ReturnsListOfScooters()
        {
            // Arrange
            GetScootersHandler handler = new GetScootersHandler(Data.CompanyRepository.Object);

            // Act
            IList<Scooter> result = handler.Handle(Data.Company.Id);

            // Assert
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public void GetScooterById_ReturnsOneScooter()
        {
            // Arrange
            GetScooterByIdHandler handler = new GetScooterByIdHandler(Data.CompanyRepository.Object, Data.GetScooterByIdValidator.Object);

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
