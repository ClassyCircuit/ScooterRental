using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Services.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Infrastructure.Services
{
    public class ScooterRepository : IScooterRepository
    {
        private readonly ICompanyRepository companyRepository;

        public ScooterRepository(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void AddScooter(string companyId, string id, decimal pricePerMinute)
        {
            Company company = companyRepository.GetCompanyById(companyId);
            var scooter = ScooterBuilder.Default(company).WithId(id).WithPricePerMinute(pricePerMinute).Build();
            company.Scooters.Add(scooter);
        }

        public Scooter GetScooterById(string companyId, string scooterId)
        {
            try
            {
                return companyRepository.GetCompanyById(companyId).Scooters.Single(x => x.Id == scooterId);
            }
            catch (InvalidOperationException)
            {
                // Missing entities are handled by domain layer.
                return null;
            }
        }

        public IList<Scooter> GetScooters(string companyId)
        {
            return companyRepository.GetCompanyById(companyId).Scooters;
        }

        public void RemoveScooter(string companyId, string id)
        {
            Company company = companyRepository.GetCompanyById(companyId);
            company.Scooters.Remove(company.Scooters.Single(x => x.Id == id));
        }

        public void UpdateScooter(string companyId, Scooter scooter)
        {
            var existingScooter = GetScooterById(companyId, scooter.Id);
            existingScooter.IsRented = scooter.IsRented;
        }
    }
}
