using ScooterRental.Core.Entities;
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
        public delegate void RentEndedHandler();
        public static event RentEndedHandler OnRentEnd;

        private readonly IEndRentValidator validator;
        private readonly IGetScooterByIdHandler getScooterByIdHandler;
        private readonly IRentalCostService rentalCostService;
        private readonly ICompanyRepository companyRepository;
        private readonly IRentEventUpdateHandler rentEventUpdateHandler;

        public EndRentHandler(IEndRentValidator endRentHandlerValidator, IGetScooterByIdHandler getScooterByIdHandler, IRentalCostService rentalCostService, ICompanyRepository companyRepository, IRentEventUpdateHandler rentEventUpdateHandler)
        {
            validator = endRentHandlerValidator;
            this.getScooterByIdHandler = getScooterByIdHandler;
            this.rentalCostService = rentalCostService;
            this.companyRepository = companyRepository;
            this.rentEventUpdateHandler = rentEventUpdateHandler;
        }

        /// <summary>
        /// Handles stopping of an active rental for a scooter.
        /// </summary>
        /// <param name="scooterId">Scooter id for which to stop rent.</param>
        /// <param name="companyId">Comapny to which the scooter belongs to.</param>
        /// <returns></returns>
        public decimal Handle(string scooterId, string companyId, DateTime endDate)
        {
            var scooter = getScooterByIdHandler.Handle(scooterId, companyId);
            validator.Validate(scooter);

            IList<RentEvent> rentEvents = CalculateRentalCostsForScooter(scooterId, companyId, endDate);
            rentEventUpdateHandler.Handle(companyId, rentEvents);

            decimal totalCost = rentEvents.GetRentEventTotalCosts();

            DisableIsRentedOnScooter(companyId, scooter);

            return totalCost;
        }

        private IList<RentEvent> CalculateRentalCostsForScooter(string scooterId, string companyId, DateTime endDate)
        {
            RentEvent rentEvent = companyRepository.GetActiveRentEventByScooterId(companyId, scooterId);
            IList<RentEvent> rentEvents = rentalCostService.Calculate(rentEvent, endDate);
            return rentEvents;
        }

        private void DisableIsRentedOnScooter(string companyId, Scooter scooter)
        {
            scooter.IsRented = false;
            companyRepository.UpdateScooter(companyId, scooter);
        }

    }
}
