using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using ScooterRental.Core.Usecases;
using ScooterRental.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests
{
    public class GetScooters
    {
        private readonly MockObjects context;

        public GetScooters(MockObjects context)
        {
            this.context = context;
        }

        [Fact]
        public void GetAllScooters_ReturnsListOfScooters()
        {
            // Arrange
            GetScootersHandler handler = new GetScootersHandler(context.ScooterService.Object);

            // Act
            IList<Scooter> result = handler.Handle();

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
