﻿using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Services;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class EndRentHandler : IEndRentHandler
    {
        private readonly IEndRentValidator validator;
        private readonly IGetScooterByIdHandler getScooterByIdHandler;
        private readonly IRentalCostService rentalCostService;
        private readonly IRentEventRepository rentEventRepository;
        private readonly IScooterRepository scooterRepository;
        private readonly IRentEventUpdateHandler rentEventUpdateHandler;
        private readonly IBusinessLogicRepository businessLogicRepository;

        public EndRentHandler(IEndRentValidator endRentHandlerValidator, IGetScooterByIdHandler getScooterByIdHandler, IRentalCostService rentalCostService, IRentEventRepository rentEventRepository, IRentEventUpdateHandler rentEventUpdateHandler, IScooterRepository scooterRepository, IBusinessLogicRepository businessLogicRepository)
        {
            validator = endRentHandlerValidator;
            this.getScooterByIdHandler = getScooterByIdHandler;
            this.rentalCostService = rentalCostService;
            this.rentEventRepository = rentEventRepository;
            this.rentEventUpdateHandler = rentEventUpdateHandler;
            this.scooterRepository = scooterRepository;
            this.businessLogicRepository = businessLogicRepository;
        }

        /// <summary>
        /// Handles stopping of an active rental for a scooter.
        /// </summary>
        /// <param name="scooterId">Scooter id for which to stop rent.</param>
        /// <param name="companyId">Comapny to which the scooter belongs to.</param>
        /// <returns></returns>
        public decimal Handle(string scooterId, string companyId, DateTime endDate)
        {
            // Find existing scooter
            var scooter = getScooterByIdHandler.Handle(scooterId, companyId);
            validator.Validate(scooter);

            // Calculate rental costs for scooter
            IList<RentEvent> rentEvents = CalculateRentalCostsForScooter(scooterId, companyId, endDate);
            rentEventUpdateHandler.Handle(companyId, rentEvents);
            decimal totalCost = rentEvents.GetRentEventTotalCosts();

            // Set scooter as available for renting
            DisableIsRentedOnScooter(companyId, scooter);

            return totalCost;
        }

        /// <summary>
        /// Calculate rental costs for scooter
        /// </summary>
        /// <param name="scooterId"></param>
        /// <param name="companyId"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private IList<RentEvent> CalculateRentalCostsForScooter(string scooterId, string companyId, DateTime endDate)
        {
            RentEvent rentEvent = rentEventRepository.GetActiveRentEventByScooterId(companyId, scooterId);
            var priceLimit = businessLogicRepository.GetPriceLimits(companyId);
            IList<RentEvent> rentEvents = rentalCostService.Calculate(rentEvent, endDate, priceLimit);
            return rentEvents;
        }

        /// <summary>
        /// Set IsRented flag to false.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="scooter"></param>
        private void DisableIsRentedOnScooter(string companyId, Scooter scooter)
        {
            scooter.IsRented = false;
            scooterRepository.UpdateScooter(companyId, scooter);
        }

    }
}
