using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using System;

namespace ScooterRental.Core.Usecases.StartRent
{
    public class StartRentHandler : IStartRentHandler
    {
        readonly IStartRentValidator validator;
        readonly IScooterService scooterService;
        readonly IRentEventService rentEventService;

        public StartRentHandler(IStartRentValidator validator, IScooterService scooterService, IRentEventService rentEventService)
        {
            this.validator = validator;
            this.scooterService = scooterService;
            this.rentEventService = rentEventService;
        }

        public void Handle(string id)
        {
            var scooter = validator.Validate(id);
            SetIsRentedFlag(scooter);
            CreateRentEvent(scooter);
        }

        private void CreateRentEvent(Scooter scooter)
        {
            rentEventService.CreateEvent(new RentEvent(
                id: Guid.NewGuid().ToString(),
                startDate: DateTime.UtcNow,
                endDate: null,
                pricePerMinute: scooter.PricePerMinute,
                isActive: true,
                totalPrice: 0m));
        }

        private void SetIsRentedFlag(Scooter scooter)
        {
            scooter.IsRented = true;
            scooterService.UpdateScooter(scooter);
        }
    }
}
