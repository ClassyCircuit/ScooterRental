using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Infrastructure.Services
{
    /// <summary>
    /// Repository persists domain model in-memory.
    /// Company is the Aggregate Root in domain model, so only one repository per aggregate root should exist.
    /// It is possible to split Company, RentEvents and Scooters into 3 aggregate roots with 3 value-objects, but that is unnecessary complication, since there are no entities lower in hierarchy.
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        Context context;

        public CompanyRepository(Context context)
        {
            this.context = context;
        }

        public Company GetCompanyByName(string name)
        {
            return context.Company.Single(x => x.Name == name);
        }

        public Company GetCompanyById(string companyId)
        {
            return context.Company.Single(x => x.Id == companyId);
        }

        public void AddScooter(string companyId, string id, decimal pricePerMinute)
        {
            Company company = GetCompanyById(companyId);
            company.Scooters.Add(new Scooter(id, pricePerMinute, company));
        }

        public Scooter GetScooterById(string companyId, string scooterId)
        {
            try
            {
                return GetCompanyById(companyId).Scooters.Single(x => x.Id == scooterId);
            }
            catch (InvalidOperationException)
            {
                // Missing entities are handled by domain layer.
                return null;
            }
        }

        public IList<Scooter> GetScooters(string companyId)
        {
            return GetCompanyById(companyId).Scooters;
        }

        public void RemoveScooter(string companyId, string id)
        {
            Company company = GetCompanyById(companyId);
            company.Scooters.Remove(company.Scooters.Single(x => x.Id == id));
        }

        public void UpdateScooter(string companyId, Scooter scooter)
        {
            var existingScooter = GetScooterById(companyId, scooter.Id);
            existingScooter.IsRented = scooter.IsRented;
        }

        public void CreateRentEvent(string companyId, RentEvent rentEvent)
        {
            GetCompanyById(companyId).RentEvents.Add(rentEvent);
        }

        public RentEvent GetRentEventById(string companyId, string rentEventId)
        {
            return GetCompanyById(companyId).RentEvents.Single(x => x.Id == rentEventId);
        }

        public void UpsertRentEvent(string companyId, RentEvent updatedEvent)
        {
            var existingEvent = GetRentEventById(companyId, updatedEvent.Id);

            existingEvent.StartDate = updatedEvent.StartDate;
            existingEvent.EndDate = updatedEvent.EndDate;
            existingEvent.PricePerMinute = updatedEvent.PricePerMinute;
            existingEvent.IsActive = updatedEvent.IsActive;
            existingEvent.TotalPrice = updatedEvent.TotalPrice;
        }

        public RentEvent GetActiveRentEventByScooterId(string companyId, string scooterId)
        {
            Company company = GetCompanyById(companyId);
            return company.RentEvents.Single(x => x.ScooterId == scooterId && x.IsActive == true);
        }
    }
}
