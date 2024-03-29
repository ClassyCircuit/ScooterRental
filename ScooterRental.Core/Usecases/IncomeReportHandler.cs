﻿using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Services;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class IncomeReportHandler : IIncomeReportHandler
    {
        private readonly IRentEventRepository repository;
        private readonly IRentalCostService rentalCostService;
        private IBusinessLogicRepository businessLogicRepository;

        public IncomeReportHandler(IRentEventRepository repository, IRentalCostService rentalCostService, IBusinessLogicRepository businessLogicRepository)
        {
            this.repository = repository;
            this.rentalCostService = rentalCostService;
            this.businessLogicRepository = businessLogicRepository;
        }

        public decimal Handle(int? year, bool includeNotCompletedRentals, string companyId, DateTime endDate)
        {
            decimal completedRentalCosts = GetCompletedEventCosts(companyId, year);
            PriceLimit priceLimit = businessLogicRepository.GetPriceLimits(companyId);

            if (includeNotCompletedRentals)
            {
                completedRentalCosts += GetActiveEventCosts(companyId, year, endDate, priceLimit);
            }

            return completedRentalCosts;
        }

        /// <summary>
        /// If active events are requested to be included in the report, then retrieve them and calculate their costs.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="year"></param>
        /// <param name="endDate"></param>
        /// <param name="priceLimit"></param>
        /// <returns></returns>
        private decimal GetActiveEventCosts(string companyId, int? year, DateTime endDate, PriceLimit priceLimit)
        {
            IList<RentEvent> activeEvents = repository.GetActiveEventsByYear(companyId, year);
            decimal activeEventTotalCosts = 0m;

            foreach (var activeEvent in activeEvents)
            {
                IList<RentEvent> result = rentalCostService.Calculate(activeEvent, endDate, priceLimit);
                activeEventTotalCosts += result.GetRentEventTotalCosts();
            }

            return activeEventTotalCosts;
        }

        /// <summary>
        /// Retrieves all completed rentals and sums up their costs.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private decimal GetCompletedEventCosts(string companyId, int? year)
        {
            IList<RentEvent> completedRentals = repository.GetCompletedRentalsByYear(companyId, year);

            return completedRentals.GetRentEventTotalCosts();

        }
    }
}
