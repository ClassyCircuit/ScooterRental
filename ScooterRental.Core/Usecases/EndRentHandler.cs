using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Services;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class EndRentHandler : IEndRentHandler
    {
        public delegate void RentEndedHandler();
        public static event RentEndedHandler OnRentEnd;

        private readonly IEndRentHandlerValidator validator;
        private readonly IGetScooterByIdHandler getScooterByIdHandler;
        private readonly ICostCalculatorService costCalculatorService;
        private readonly ICompanyRepository companyRepository;
        private readonly IRentEventUpdateHandler rentEventUpdateHandler;

        public EndRentHandler(IEndRentHandlerValidator endRentHandlerValidator, IGetScooterByIdHandler getScooterByIdHandler, ICostCalculatorService costCalculatorService, ICompanyRepository companyRepository, IRentEventUpdateHandler rentEventUpdateHandler)
        {
            validator = endRentHandlerValidator;
            this.getScooterByIdHandler = getScooterByIdHandler;
            this.costCalculatorService = costCalculatorService;
            this.companyRepository = companyRepository;
            this.rentEventUpdateHandler = rentEventUpdateHandler;
        }

        /// <summary>
        /// Handles stopping of an active rental for a scooter.
        /// </summary>
        /// <param name="scooterId">Scooter id for which to stop rent.</param>
        /// <param name="companyId">Comapny to which the scooter belongs to.</param>
        /// <returns></returns>
        public decimal Handle(string scooterId, string companyId)
        {
            var scooter = getScooterByIdHandler.Handle(scooterId, companyId);
            validator.Validate(scooter);

            IList<RentEvent> rentEvents = CalculateRentCostsForScooter(scooterId, companyId);
            PersistCostsInStorage(companyId, rentEvents);
            decimal totalCost = rentEvents.GetRentEventTotalCosts();

            DisableIsRentedOnScooter(companyId, scooter);

            return totalCost;
        }

        private void PersistCostsInStorage(string companyId, IList<RentEvent> rentEvents)
        {
            rentEventUpdateHandler.Handle(companyId, rentEvents);
        }

        private IList<RentEvent> CalculateRentCostsForScooter(string scooterId, string companyId)
        {
            RentEvent rentEvent = companyRepository.GetActiveRentEventByScooterId(companyId, scooterId);
            IList<RentEvent> rentEvents = costCalculatorService.CalculateRentEventCosts(rentEvent);
            return rentEvents;
        }

        private void DisableIsRentedOnScooter(string companyId, Scooter scooter)
        {
            scooter.IsRented = false;
            companyRepository.UpdateScooter(companyId, scooter);
        }

    }
}
