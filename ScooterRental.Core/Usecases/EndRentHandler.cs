using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases
{
    public class EndRentHandler : IEndRentHandler
    {
        private readonly IEndRentHandlerValidator validator;
        private readonly IGetScooterByIdHandler getScooterByIdHandler;
        private readonly ICostCalculatorService costCalculatorService;
        private readonly ICompanyRepository companyRepository;

        public EndRentHandler(IEndRentHandlerValidator endRentHandlerValidator, IGetScooterByIdHandler getScooterByIdHandler, ICostCalculatorService costCalculatorService, ICompanyRepository companyRepository)
        {
            validator = endRentHandlerValidator;
            this.getScooterByIdHandler = getScooterByIdHandler;
            this.costCalculatorService = costCalculatorService;
            this.companyRepository = companyRepository;
        }

        public decimal Handle(string scooterId, string companyId)
        {
            var scooter = getScooterByIdHandler.Handle(scooterId, companyId);
            validator.Validate(scooter);

            RentEvent rentEvent = companyRepository.GetActiveRentEventByScooterId(companyId, scooterId);
            decimal totalCost = costCalculatorService.GetCostFor(rentEvent);

            UpdateRentEvent(companyId, rentEvent, totalCost);
            DisableIsRentedOnScooter(companyId, scooter);

            return totalCost;
        }

        private void UpdateRentEvent(string companyId, RentEvent rentEvent, decimal totalCost)
        {
            rentEvent.TotalPrice = totalCost;
            rentEvent.IsActive = false;
            companyRepository.UpdateRentEvent(companyId, rentEvent);
        }

        private void DisableIsRentedOnScooter(string companyId, Scooter scooter)
        {
            scooter.IsRented = false;
            companyRepository.UpdateScooter(companyId, scooter);
        }

    }
}
