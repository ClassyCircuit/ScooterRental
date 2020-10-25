using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using System;

namespace ScooterRental.Core.Usecases
{
    public class StartRentHandler : IStartRentHandler
    {
        readonly IStartRentValidator validator;
        readonly ICompanyRepository companyRepository;

        public StartRentHandler(IStartRentValidator validator, ICompanyRepository companyRepository)
        {
            this.validator = validator;
            this.companyRepository = companyRepository;
        }

        public void Handle(string id, Company company)
        {
            var scooter = validator.Validate(id, company.Id);
            SetIsRentedFlag(scooter, company.Id);
            CreateRentEvent(scooter, company);
        }

        private void CreateRentEvent(Scooter scooter, Company company)
        {
            companyRepository.CreateRentEvent(company.Id, new RentEvent(
                id: Guid.NewGuid().ToString(),
                startDate: DateTime.UtcNow,
                endDate: null,
                pricePerMinute: scooter.PricePerMinute,
                isActive: true,
                company: company));
        }

        private void SetIsRentedFlag(Scooter scooter, string companyId)
        {
            scooter.IsRented = true;
            companyRepository.UpdateScooter(companyId, scooter);
        }
    }
}
