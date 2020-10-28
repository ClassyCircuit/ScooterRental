using ScooterRental.Core.Entities;
using ScooterRental.Core.Services;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class CostCalculatorServiceTests : TestBase
    {
        public CostCalculatorServiceTests(Mocks mocks) : base(mocks)
        {
        }

        [Fact]
        public void GetCosts_ForLessThan20EUR_ReturnsOneRentEvent()
        {
            var scooter = ScooterBuilder.Default(Mocks.Company).WithPricePerMinute(2m).Build();
            DateTime startDate = DateTime.Today.AddSeconds(-30);
            var rentEvent = RentEventBuilder.Default(Mocks.Company, scooter).WithStartDate(DateTime.UtcNow.AddHours(-7)).Build();
            CostCalculatorService service = new CostCalculatorService();

            IList<RentEvent> updatedEvents = service.CalculateRentEventCosts(rentEvent);
            updatedEvents.Count.ShouldBe(1);
            updatedEvents[0].TotalPrice.ShouldBe()
        }
    }
}
