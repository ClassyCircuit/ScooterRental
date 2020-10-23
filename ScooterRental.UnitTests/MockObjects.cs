using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using ScooterRental.Core.Usecases.AddScooter;
using System.Collections.Generic;

namespace ScooterRental.UnitTests
{
    /// <summary>
    /// Holds re-usable mock objects for unit test arrange stage.
    /// </summary>
    public class MockObjects
    {
        public Mock<IScooterService> ScooterService { get; }
        public Mock<AddScooterValidator> AddScooterValidator { get; }

        public MockObjects()
        {
            IList<Scooter> scooters = new List<Scooter>()
            {
                new Scooter("1", 0.5m)
            };

            ScooterService = new Mock<IScooterService>();
            ScooterService.Setup(x => x.GetScooters()).Returns(scooters);
            ScooterService.Setup(x=>x.GetScooterById("1")).Returns(scooters[0]);

            //AddScooterValidator = new Mock<AddScooterValidator>().Setup()
        }
    }
}
