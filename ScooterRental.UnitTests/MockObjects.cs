using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using ScooterRental.Core.Usecases.AddScooter;
using ScooterRental.UnitTests.Builders;
using System.Collections.Generic;

namespace ScooterRental.UnitTests
{
    /// <summary>
    /// Holds re-usable mock objects for setting up unit tests.
    /// </summary>
    public class Context
    {
        public Mock<IScooterService> ScooterService { get; }
        public Mock<AddScooterValidator> AddScooterValidator { get; }
        public IList<Scooter> scooters { get; }

        public string ExistingScooterId { get; }

        public Context()
        {
            scooters = new List<Scooter>()
            {
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
            };

            ExistingScooterId = scooters[0].Id;

            ScooterService = new Mock<IScooterService>();
            ScooterService.Setup(x => x.GetScooters()).Returns(scooters);
            ScooterService.Setup(x => x.GetScooterById(ExistingScooterId)).Returns(scooters[0]);

        }
    }
}
