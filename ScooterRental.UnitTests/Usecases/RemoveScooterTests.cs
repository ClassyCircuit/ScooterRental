using ScooterRental.Core.Usecases.RemoveScooter;
using ScooterRental.Core.Exceptions;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System.Linq;
using Xunit;
using System;

namespace ScooterRental.UnitTests.Usecases
{
    public class RemoveScooterTests : TestBase
    {
        public RemoveScooterTests(Context context) : base(context)
        {
        }

        [Fact]
        public void RemoveScooterValidator_IsRented_ThrowsException()
        {
            // Arrange
            RemoveScooterValidator validator = new RemoveScooterValidator(Context.ScooterService.Object);
            var scooter = Context.Scooters.First(x => x.Id == Context.ExistingScooterId);
            scooter.IsRented = true;

            // Act
            Action act = () => validator.Validate(Context.ExistingScooterId);

            // Assert
            Should.Throw<RentedScooterCannotBeRemovedException>(act);
        }

        [Fact]
        public void RemoveScooterValidator_InvalidId_ThrowsException()
        {
            // Arrange
            RemoveScooterValidator validator = new RemoveScooterValidator(Context.ScooterService.Object);

            // Act
            Action act = () => validator.Validate("");

            // Assert
            Should.Throw<IdCannotBeEmptyException>(act);
        }
    }
}
