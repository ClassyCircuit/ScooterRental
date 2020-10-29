using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using System;

namespace ScooterRental.Core.Usecases
{
    public class StartRentHandler : IStartRentHandler
    {
        private readonly IStartRentValidator validator;
        private readonly IRentEventRepository rentEventRepository;
        private readonly IScooterRepository scooterRepository;

        public StartRentHandler(IStartRentValidator validator, IRentEventRepository rentEventRepository, IScooterRepository scooterRepository)
        {
            this.validator = validator;
            this.rentEventRepository = rentEventRepository;
            this.scooterRepository = scooterRepository;
        }

        public void Handle(string id, Company company)
        {
            var scooter = validator.Validate(id, company.Id);
            SetIsRentedFlag(scooter, company.Id);
            CreateRentEvent(scooter, company);
        }

        /// <summary>
        /// Create a new rental event when starting a rent.
        /// </summary>
        /// <param name="scooter"></param>
        /// <param name="company"></param>
        private void CreateRentEvent(Scooter scooter, Company company)
        {
            rentEventRepository.CreateRentEvent(company.Id, new RentEvent(
                id: Guid.NewGuid().ToString(),
                startDate: DateTime.UtcNow,
                endDate: null,
                pricePerMinute: scooter.PricePerMinute,
                isActive: true,
                company: company,
                scooterId: scooter.Id));
        }

        /// <summary>
        /// Mark the scooter as unavailable for renting.
        /// </summary>
        /// <param name="scooter"></param>
        /// <param name="companyId"></param>
        private void SetIsRentedFlag(Scooter scooter, string companyId)
        {
            scooter.IsRented = true;
            scooterRepository.UpdateScooter(companyId, scooter);
        }
    }
}
