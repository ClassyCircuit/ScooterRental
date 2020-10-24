using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Usecases.AddScooter;
using ScooterRental.Core.Usecases.GetScooterById;
using ScooterRental.Core.Usecases.RemoveScooter;
using ScooterRental.UnitTests.Builders;
using System.Collections.Generic;

namespace ScooterRental.UnitTests.Setup
{
    /// <summary>
    /// Holds re-usable mock objects for setting up unit tests.
    /// </summary>
    public class Context
    {
        public Mock<IScooterService> ScooterService { get; }
        public IList<Scooter> Scooters { get; }
        public string ExistingScooterId { get; }
        public Mock<GetScooterByIdValidator> GetScooterByIdValidator { get; internal set; }

        public Context()
        {
            Scooters = new List<Scooter>()
            {
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
                ScooterBuilder.Default().Build(),
            };

            ExistingScooterId = Scooters[0].Id;

            ScooterService = new Mock<IScooterService>();
            ScooterService.Setup(x => x.GetScooters()).Returns(Scooters);
            ScooterService.Setup(x => x.GetScooterById(ExistingScooterId)).Returns(Scooters[0]);

            GetScooterByIdValidator = new Mock<GetScooterByIdValidator>();
        }
    }
}
