using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Usecases;
using ScooterRental.UnitTests.Builders;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class IncomeReportTests : TestBase
    {
        Mock<IRentalCostService> rentalCostService;
        IList<RentEvent> completedEvents;
        IList<RentEvent> activeEvents;
        IList<RentEvent> calculatedActiveEvents;
        DateTime startDateYesterday = DateTime.Today.Date.AddMinutes(-10);
        DateTime startDateOneYearAgo = DateTime.Today.Date.AddYears(-1);
        DateTime endDate = DateTime.Today.Date.AddMinutes(5);
        decimal price = 1.5m;


        public IncomeReportTests(Data data) : base(data)
        {
            rentalCostService = new Mock<IRentalCostService>();

            completedEvents = new List<RentEvent>()
            {
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateYesterday).WithTotalPrice(5).WithIsActive(false).Build(),
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateYesterday).WithTotalPrice(5).WithIsActive(false).Build(),
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateOneYearAgo).WithTotalPrice(5).WithIsActive(false).Build(),
            };

            activeEvents = new List<RentEvent>()
            {
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateYesterday).WithEndDate(endDate).WithPricePerMinute(price).WithIsActive(true).Build(),
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateYesterday).WithEndDate(endDate).WithPricePerMinute(price).WithIsActive(true).Build()
            };

            calculatedActiveEvents = new List<RentEvent>()
            {
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateYesterday).WithEndDate(endDate).WithPricePerMinute(price).WithIsActive(true).WithTotalPrice(4.2m).Build(),
                RentEventBuilder.Default(Data.Company, Data.Scooters[0]).WithStartDate(startDateYesterday).WithEndDate(endDate).WithPricePerMinute(price).WithIsActive(true).WithTotalPrice(4.2m).Build()
            };

        }

        [Fact]
        public void Handler_CompletedRents_ReturnsCostOfCompletedEvents()
        {
            Data.CompanyRepository.Setup(x => x.GetCompletedRentalsByYear(Data.Company.Id, null)).Returns(completedEvents);
            IncomeReportHandler handler = new IncomeReportHandler(Data.CompanyRepository.Object, rentalCostService.Object);

            decimal result = handler.Handle(null, false, Data.Company.Id, startDateYesterday);

            result.ShouldBe(5 * completedEvents.Count);
        }

        [Fact]
        public void Handler_AllRents_ReturnsCostOfActiveAndCompletedEvents()
        {
            Data.CompanyRepository.Setup(x => x.GetCompletedRentalsByYear(Data.Company.Id, null)).Returns(completedEvents);
            Data.CompanyRepository.Setup(x => x.GetActiveEventsByYear(Data.Company.Id, null)).Returns(activeEvents);

            foreach (var activeEvent in activeEvents)
            {
                rentalCostService.Setup(x => x.Calculate(activeEvent, endDate)).Returns(calculatedActiveEvents);

            }

            IncomeReportHandler handler = new IncomeReportHandler(Data.CompanyRepository.Object, rentalCostService.Object);

            decimal result = handler.Handle(null, true, Data.Company.Id, endDate);

            decimal completedEventCost = 5m * completedEvents.Count;
            decimal activeEventCost = 4.2m * 4; // There are 2 resulting events from each active event, so 4 in total

            result.ShouldBe(completedEventCost + activeEventCost);
        }

        [Fact]
        public void Handler_CompletedRentsOneYearAgo_ReturnsCostOfEventsOneYearAgo()
        {
            int year = startDateOneYearAgo.Year;
            IList<RentEvent> eventsYearAgo = new List<RentEvent>()
            {
                completedEvents[2]
            };

            Data.CompanyRepository.Setup(x => x.GetCompletedRentalsByYear(Data.Company.Id, year)).Returns(eventsYearAgo);
            IncomeReportHandler handler = new IncomeReportHandler(Data.CompanyRepository.Object, rentalCostService.Object);

            decimal result = handler.Handle(year, false, Data.Company.Id, startDateYesterday);

            result.ShouldBe(5);
        }
    }
}
