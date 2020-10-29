using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Services;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class IncomeReportHandler : IIncomeReportHandler
    {
        private readonly ICompanyRepository repository;
        private readonly IRentalCostService rentalCostService;

        public IncomeReportHandler(ICompanyRepository repository, IRentalCostService rentalCostService)
        {
            this.repository = repository;
            this.rentalCostService = rentalCostService;
        }

        public decimal Handle(int? year, bool includeNotCompletedRentals, string companyId, DateTime endDate)
        {
            decimal completedRentalCosts = GetCompletedEventCosts(companyId, year);

            if (includeNotCompletedRentals)
            {
                completedRentalCosts += GetActiveEventCosts(companyId, year, endDate);
            }

            return completedRentalCosts;
        }

        private decimal GetActiveEventCosts(string companyId, int? year, DateTime endDate)
        {
            IList<RentEvent> activeEvents = repository.GetActiveEventsByYear(companyId, year);
            decimal activeEventTotalCosts = 0m;

            foreach (var activeEvent in activeEvents)
            {
                IList<RentEvent> result = rentalCostService.Calculate(activeEvent, endDate);
                activeEventTotalCosts += result.GetRentEventTotalCosts();
            }

            return activeEventTotalCosts;
        }

        private decimal GetCompletedEventCosts(string companyId, int? year)
        {
            IList<RentEvent> completedRentals = repository.GetCompletedRentalsByYear(companyId, year);

            return completedRentals.GetRentEventTotalCosts();

        }
    }
}
