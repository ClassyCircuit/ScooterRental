using ScooterRental.Core.Entities;
using ScooterRental.Infrastructure;
using ScooterRental.UnitTests.Builders;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class ScooterServiceTests
    {
        private readonly Context context;

        public ScooterServiceTests(Context context)
        {
            this.context = context;
        }

        [Fact]
        public void AddScooter_AddsNewObjectToList()
        {
            // Arrange          
            ScooterService service = new ScooterService(context.scooters);
            int scooterCountBefore = context.scooters.Count;

            string scooterId = GetRandom.UniqueId();
            decimal price = GetRandom.Decimal(0, 10);

            // Act
            service.AddScooter(scooterId, price);

            // Assert
            context.scooters.Count.ShouldBe(scooterCountBefore + 1);
        }

        [Fact]
        public void GetScooterById_InvalidId_ReturnsNull()
        {
            // Arrange
            ScooterService service = new ScooterService(context.scooters);
            
            // Act
            Scooter result = service.GetScooterById("");

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public void GetScooters_ReturnsListOfScooters()
        {
            ScooterService service = new ScooterService(context.scooters);

            // Act
            IList<Scooter> scooters = service.GetScooters();

            // Assert
            scooters.ShouldNotBeEmpty();
        }

        [Fact]
        public void RemoveScooter_RemovesObjectFromList()
        {
            // Arrange
            ScooterService service = new ScooterService(context.scooters);
            int scooterCountBefore = context.scooters.Count;
            
            // Act
            service.RemoveScooter(context.ExistingScooterId);

            // Assert
            context.scooters.Count.ShouldBe(scooterCountBefore - 1);
        }

        [Fact]
        public void RemoveScooter_InvalidId_DoesNotRemoveAnything()
        {
            // Arrange
            ScooterService service = new ScooterService(context.scooters);
            int scooterCountBefore = context.scooters.Count;

            // Act
            service.RemoveScooter("");

            // Assert
            context.scooters.Count.ShouldBe(scooterCountBefore);
        }
    }
}
