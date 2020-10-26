using ScooterRental.Core.Entities;
using ScooterRental.Infrastructure.Data;
using ScooterRental.Infrastructure.Services;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class CompanyRepositoryTests : TestBase
    {
        private Context context;
        private CompanyRepository repository;

        public CompanyRepositoryTests(Mocks mocks) : base(mocks)
        {
            List<Company> companies = new List<Company>()
            {
                mocks.Company
            };

            context = new Context(companies);
            repository = new CompanyRepository(context);
        }

        [Fact]
        public void AddScooter_AddsNewObjectToList()
        {
            // Arrange          
            int scooterCountBefore = Mocks.Scooters.Count;

            string scooterId = GetRandom.UniqueId();
            decimal price = GetRandom.Decimal(0, 10);

            // Act
            repository.AddScooter(Mocks.Company.Id, scooterId, price);

            // Assert
            Mocks.Scooters.Count.ShouldBe(scooterCountBefore + 1);
        }

        [Fact]
        public void GetScooterById_InvalidId_ReturnsNull()
        {
            // Act
            Scooter result = repository.GetScooterById(Mocks.Company.Id, "");

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public void GetScooters_ReturnsListOfScooters()
        {
            // Act
            IList<Scooter> scooters = repository.GetScooters(Mocks.Company.Id);

            // Assert
            scooters.ShouldNotBeEmpty();
        }

        [Fact]
        public void RemoveScooter_RemovesObjectFromList()
        {
            // Arrange
            int scooterCountBefore = Mocks.Scooters.Count;

            // Act
            repository.RemoveScooter(Mocks.Company.Id, Mocks.ExistingScooterId);

            // Assert
            Mocks.Scooters.Count.ShouldBe(scooterCountBefore - 1);
        }

        [Fact]
        public void RemoveScooter_InvalidId_ThrowsException()
        {
            // Arrange
            Action act = () => repository.RemoveScooter(Mocks.Company.Id, "");

            // Act & Assert
            Should.Throw<InvalidOperationException>(act);
        }

        [Fact]
        public void UpdateScooter_ExistingScooterValuesChanged()
        {
            // Arrange
            var scooter = Mocks.Scooters[0];
            scooter.IsRented = false;

            var updated = ScooterBuilder
                .Default(Mocks.Company)
                .WithId(scooter.Id)
                .WithPricePerMinute(scooter.PricePerMinute)
                .WithIsRented(true)
                .Build();

            // Act
            repository.UpdateScooter(Mocks.Company.Id, updated);

            // Assert
            Mocks.Scooters[0].IsRented.ShouldBe(true);
        }
    }
}
