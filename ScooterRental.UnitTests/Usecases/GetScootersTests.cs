using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Usecases.GetScooterById;
using ScooterRental.Core.Usecases.GetScooters;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class GetScootersTests : TestBase
    {
        public GetScootersTests(Context context) : base(context)
        {
        }

        [Fact]
        public void GetAllScooters_ReturnsListOfScooters()
        {
            // Arrange
            GetScootersHandler handler = new GetScootersHandler(Context.ScooterService.Object);

            // Act
            IList<Scooter> result = handler.Handle();

            // Assert
            result.ShouldNotBeEmpty();
        }

        [Fact]
        public void GetScooterById_ReturnsOneScooter()
        {
            // Arrange
            GetScooterByIdHandler handler = new GetScooterByIdHandler(Context.ScooterService.Object, Context.GetScooterByIdValidator.Object);

            // Act
            Scooter result = handler.Handle(Context.ExistingScooterId);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Context.ExistingScooterId);
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
