using ScooterRental.Core.Entities;
using ScooterRental.Infrastructure;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class ScooterServiceTests : TestBase
    {
        public ScooterServiceTests(Context context) : base(context)
        {
        }

        [Fact]
        public void AddScooter_AddsNewObjectToList()
        {
            // Arrange          
            ScooterService service = new ScooterService(Context.Scooters);
            int scooterCountBefore = Context.Scooters.Count;

            string scooterId = GetRandom.UniqueId();
            decimal price = GetRandom.Decimal(0, 10);

            // Act
            service.AddScooter(scooterId, price);

            // Assert
            Context.Scooters.Count.ShouldBe(scooterCountBefore + 1);
        }

        [Fact]
        public void GetScooterById_InvalidId_ReturnsNull()
        {
            // Arrange
            ScooterService service = new ScooterService(Context.Scooters);
            
            // Act
            Scooter result = service.GetScooterById("");

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public void GetScooters_ReturnsListOfScooters()
        {
            ScooterService service = new ScooterService(Context.Scooters);

            // Act
            IList<Scooter> scooters = service.GetScooters();

            // Assert
            scooters.ShouldNotBeEmpty();
        }

        [Fact]
        public void RemoveScooter_RemovesObjectFromList()
        {
            // Arrange
            ScooterService service = new ScooterService(Context.Scooters);
            int scooterCountBefore = Context.Scooters.Count;
            
            // Act
            service.RemoveScooter(Context.ExistingScooterId);

            // Assert
            Context.Scooters.Count.ShouldBe(scooterCountBefore - 1);
        }

        [Fact]
        public void RemoveScooter_InvalidId_DoesNotRemoveAnything()
        {
            // Arrange
            ScooterService service = new ScooterService(Context.Scooters);
            int scooterCountBefore = Context.Scooters.Count;

            // Act
            service.RemoveScooter("");

            // Assert
            Context.Scooters.Count.ShouldBe(scooterCountBefore);
        }
    }
}
