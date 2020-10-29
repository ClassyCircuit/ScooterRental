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

        public RentalCostServiceTests(Data mocks) : base(mocks)
        {
            // 20 EUR limit per day.
            costLimitPerDay = 20m;
        }

        /// <summary>
        /// Set up scooter, rent event and service to use in tests.
        /// </summary>
        /// <param name="pricePerMinute"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        private void Setup(decimal pricePerMinute, DateTime startDate, DateTime endDate)
        {
            scooter = ScooterBuilder.Default(Data.Company).WithPricePerMinute(pricePerMinute).Build();
            this.endDate = endDate;
            this.startDate = startDate;

            rentEvent = RentEventBuilder.Default(Data.Company, scooter).WithStartDate(startDate).WithPricePerMinute(pricePerMinute).Build();

            service = new RentalCostService(costLimitPerDay);
        }

        private static void VerifyEventFields(decimal totalCost, DateTime endDate, RentEvent updatedEvent)
        {
            updatedEvent.TotalPrice.ShouldBe(totalCost);
            updatedEvent.EndDate.ShouldBe(endDate);
        }

        private static void VerifyEventCount(IList<RentEvent> updatedEvents, int count)
        {
            updatedEvents.Count.ShouldBe(count, $"{count} rent events should have been created");
        }

        [Fact]
        public void Calculate_LessThan20EUR_UsesPricePerMinute()
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

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
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

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayUnderLimit_SecondDayUnderLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddMinutes(-5), endDate: DateTime.Today.AddMinutes(5));

            decimal firstDayCost = 10m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 10m;
            DateTime secondDayEndDate = endDate;

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayLessThanMinute_SecondDayOverLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddSeconds(-5), endDate: DateTime.Today.AddHours(15));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 0m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 20m;
            DateTime secondDayEndDate = endDate.Date.AddMinutes(minutesTillCostLimit);

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayLessThanMinute_SecondDayLessThanMinute()
        {
            Setup(2m, startDate: DateTime.Today.AddSeconds(-5), endDate: DateTime.Today.AddSeconds(5));

            decimal firstDayCost = 0m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 0m;
            DateTime secondDayEndDate = endDate;

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayOverLimit_SecondDayOverLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddHours(-2), endDate: DateTime.Today.AddHours(3));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 20m;
            DateTime firstDayEndDate = startDate.AddMinutes(minutesTillCostLimit);

            decimal secondDayCost = 20m;
            DateTime secondDayEndDate = endDate.Date.AddMinutes(minutesTillCostLimit);

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayLessThanMinute_SecondDayUnderLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddSeconds(-59), endDate: DateTime.Today.AddMinutes(3));
            decimal firstDayCost = 0m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 6m;
            DateTime secondDayEndDate = endDate;

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayUnderLimit_SecondDayLessThanMinute()
        {
            Setup(2m, startDate: DateTime.Today.AddMinutes(-3), endDate: DateTime.Today.AddSeconds(40));
            decimal firstDayCost = 6m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 0m;
            DateTime secondDayEndDate = endDate;

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        [Fact]
        public void Calculate_FirstDayUnderLimit_SecondDayOverLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddMinutes(-6), endDate: DateTime.Today.AddHours(3));
            int minutesTillCostLimit = GetMinutesTillCostLimit();

            decimal firstDayCost = 12m;
            DateTime firstDayEndDate = DateTime.Today.Date.AddTicks(-1);

            decimal secondDayCost = 20m;
            DateTime secondDayEndDate = endDate.Date.AddMinutes(minutesTillCostLimit);

            TestTwoEvents(firstDayCost, firstDayEndDate, secondDayCost, secondDayEndDate);
        }

        private int GetMinutesTillCostLimit()
        {
            return (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);
        }

        private void TestTwoEvents(decimal firstDayCost, DateTime firstDayEndDate, decimal secondDayCost, DateTime secondDayEndDate)
        {
            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents, 2);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
        }

        [Fact]
        public void Calculate_FirstDayLessThanMinute_SecondDayOverLimit_ThirdDayUnderLimit()
        {
            Setup(2m, startDate: DateTime.Today.AddSeconds(-2), endDate: DateTime.Today.AddDays(1).AddMinutes(5));
            int minutesTillCostLimit = (int)Math.Floor(costLimitPerDay / rentEvent.PricePerMinute);

            decimal firstDayCost = 0m;
            DateTime firstDayEndDate = startDate.Date.AddDays(1).AddTicks(-1);

            decimal secondDayCost = 20m;
            DateTime secondDayEndDate = startDate.Date.AddDays(1).AddMinutes(minutesTillCostLimit);

            decimal thirdDayCost = 10m;
            DateTime thirdDayEndDate = endDate;

            IList<RentEvent> updatedEvents = service.Calculate(rentEvent, endDate);

            VerifyEventCount(updatedEvents, 3);
            VerifyEventFields(firstDayCost, firstDayEndDate, updatedEvents[0]);
            VerifyEventFields(secondDayCost, secondDayEndDate, updatedEvents[1]);
            VerifyEventFields(thirdDayCost, thirdDayEndDate, updatedEvents[2]);
        }
    }
}
