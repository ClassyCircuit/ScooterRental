using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
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
    public class ScooterRepositoryTests : TestBase
    {
        private Context context;
        private ScooterRepository scooterRepository;
        private Mock<ICompanyRepository> companyRepository;

        public ScooterRepositoryTests(Data mocks) : base(mocks)
        {
            List<Company> companies = new List<Company>()
            {
                mocks.Company
            };

            context = new Context(companies);
            companyRepository = new Mock<ICompanyRepository>();
            companyRepository.Setup(x => x.GetCompanyById(Data.Company.Id)).Returns(Data.Company);
            scooterRepository = new ScooterRepository(companyRepository.Object);
        }

        [Fact]
        public void AddScooter_AddsNewObjectToList()
        {
            // Arrange          
            int scooterCountBefore = Data.Scooters.Count;

            string scooterId = GetRandom.UniqueId();
            decimal price = GetRandom.Decimal(0, 10);

            // Act
            scooterRepository.AddScooter(Data.Company.Id, scooterId, price);

            // Assert
            Data.Scooters.Count.ShouldBe(scooterCountBefore + 1);
        }

        [Fact]
        public void GetScooterById_InvalidId_ReturnsNull()
        {
            // Act
            Scooter result = scooterRepository.GetScooterById(Data.Company.Id, "");

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public void GetScooters_ReturnsListOfScooters()
        {
            // Act
            IList<Scooter> scooters = scooterRepository.GetScooters(Data.Company.Id);

            // Assert
            scooters.ShouldNotBeEmpty();
        }

        [Fact]
        public void RemoveScooter_RemovesObjectFromList()
        {
            // Arrange
            int scooterCountBefore = Data.Scooters.Count;

            // Act
            scooterRepository.RemoveScooter(Data.Company.Id, Data.ExistingScooterId);

            // Assert
            Data.Scooters.Count.ShouldBe(scooterCountBefore - 1);
        }

        [Fact]
        public void RemoveScooter_InvalidId_ThrowsException()
        {
            // Arrange
            Action act = () => scooterRepository.RemoveScooter(Data.Company.Id, "");

            // Act & Assert
            Should.Throw<InvalidOperationException>(act);
        }

        [Fact]
        public void UpdateScooter_ExistingScooterValuesChanged()
        {
            // Arrange
            var scooter = Data.Scooters[0];
            scooter.IsRented = false;

            var updated = ScooterBuilder
                .Default(Data.Company)
                .WithId(scooter.Id)
                .WithPricePerMinute(scooter.PricePerMinute)
                .WithIsRented(true)
                .Build();

            // Act
            scooterRepository.UpdateScooter(Data.Company.Id, updated);

            // Assert
            Data.Scooters[0].IsRented.ShouldBe(true);
        }
    }
}
