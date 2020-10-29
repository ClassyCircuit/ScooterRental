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
    public class RentalCostServiceTests : TestBase
    {
        private Scooter scooter;
        private DateTime startDate;
        private DateTime endDate;
        private RentEvent rentEvent;
        private decimal costLimitPerDay;
        private decimal expectedCost;

        RentalCostService service;

        public RentalCostServiceTests(Mocks mocks) : base(mocks)
        {
            // 20 EUR limit per day.
            costLimitPerDay = 20m;
        }

        private void Setup(decimal pricePerMinute, DateTime startDate, DateTime endDate)
        {
            scooter = ScooterBuilder.Default(Mocks.Company).WithPricePerMinute(pricePerMinute).Build();
            this.endDate = endDate;
            this.startDate = startDate;

            rentEvent = RentEventBuilder.Default(Mocks.Company, scooter).WithStartDate(startDate).WithPricePerMinute(pricePerMinute).Build();

            service = new RentalCostService(costLimitPerDay);
        }

        private static void VerifyEventFields(decimal totalCost, DateTime endDate, RentEvent updatedEvent)
        {
            updatedEvent.TotalPrice.ShouldBe(totalCost);
            updatedEvent.EndDate.ShouldBe(endDate);
        }

        private static void VerifyEventCount(IList<RentEvent> updatedEvents)
        {
            updatedEvents.Count.ShouldBe(2, "Two rent events should have been created");
        }

        [Fact]
        public void Calculate_LessThan20EUR_ReturnsOneRentEvent()
        {
            Setup(2m, startDate: DateTime.Today, endDate: DateTime.Today.AddMinutes(5));
            expectedCost = rentEvent.PricePerMinute * Convert.ToDecimal((endDate - startDate).TotalMinutes);

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);
            updatedEvents.Count.ShouldBe(1);
            updatedEvents[0].TotalPrice.ShouldBe(expectedCost);
            updatedEvents[0].EndDate.ShouldBe(endDate);
        }

        [Fact]
        public void Calculate_LessThanOneMinute_ReturnsZeroCost()
        {
            Setup(2m, startDate: DateTime.Today, endDate: DateTime.Today.AddSeconds(59));
            expectedCost = 0m;

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);
            updatedEvents.Count.ShouldBe(1);
            updatedEvents[0].TotalPrice.ShouldBe(expectedCost);
            updatedEvents[0].EndDate.ShouldBe(endDate);
        }

        [Fact]
        public void Calculate_MoreThan20EUR_Returns20EURCost()
        {
            Setup(2m, startDate: DateTime.Today, endDate: DateTime.Today.AddHours(10));
            expectedCost = 20m;
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);
            DateTime actualEndDate = rentEvent.StartDate.AddMinutes(minutesTillCostLimit);

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);
            updatedEvents.Count.ShouldBe(1);
            updatedEvents[0].TotalPrice.ShouldBe(expectedCost);
            updatedEvents[0].EndDate.ShouldBe(actualEndDate);
        }

        [Fact]
        public void Calculate_FirstDayOverLimit_SecondUnderLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddMinutes(-40), endDate: DateTime.Today.AddMinutes(5));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 20m;
            DateTime firstDayEndDate = rentEvent.StartDate.AddMinutes(minutesTillCostLimit);

            decimal secondDayCost = 5 * rentEvent.PricePerMinute;
            DateTime secondDayEndDate = endDate;

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
        }

        [Fact]
        public void Calculate_FirstDayOverLimit_SecondLessThanOneMinute()
        {
            Setup(2m, startDate: DateTime.Today.AddMinutes(-40), endDate: DateTime.Today.AddSeconds(59));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 20m;
            DateTime firstDayEndDate = rentEvent.StartDate.AddMinutes(minutesTillCostLimit);

            decimal secondDayCost = 0m;
            DateTime secondDayEndDate = endDate;

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
        }

        [Fact]
        public void Calculate_FirstDayUnderLimit_SecondDayUnderLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddMinutes(-5), endDate: DateTime.Today.AddMinutes(5));

            decimal firstDayCost = 10m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 10m;
            DateTime secondDayEndDate = endDate;

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
        }
        // TODO: Refactor tests to get rid of duplication
        [Fact]
        public void Calculate_FirstDayLessThanMinute_SecondDayOverLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddSeconds(-5), endDate: DateTime.Today.AddHours(15));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 0m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 20m;
            DateTime secondDayEndDate = endDate.Date.AddMinutes(minutesTillCostLimit);

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
        }

        [Fact]
        public void Calculate_FirstDayLessThanMinute_SecondLessThanMinute()
        {
            Setup(2m, startDate: DateTime.Today.AddSeconds(-5), endDate: DateTime.Today.AddSeconds(5));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 0m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 0m;
            DateTime secondDayEndDate = endDate;

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
        }


    }
}
