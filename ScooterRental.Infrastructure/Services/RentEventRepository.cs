using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Infrastructure.Services
{
    public class RentEventRepository : IRentEventRepository
    {
        private readonly ICompanyRepository companyRepository;

        public RentEventRepository(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void CreateRentEvent(string companyId, RentEvent rentEvent)
        {
            companyRepository.GetCompanyById(companyId).RentEvents.Add(rentEvent);
        }

        public RentEvent GetRentEventById(string companyId, string rentEventId)
        {
            return companyRepository.GetCompanyById(companyId).RentEvents.Single(x => x.Id == rentEventId);
        }

        public void UpsertRentEvent(string companyId, RentEvent updatedEvent)
        {
            try
            {
                var existingEvent = GetRentEventById(companyId, updatedEvent.Id);
                existingEvent.StartDate = updatedEvent.StartDate;
                existingEvent.EndDate = updatedEvent.EndDate;
                existingEvent.PricePerMinute = updatedEvent.PricePerMinute;
                existingEvent.IsActive = updatedEvent.IsActive;
                existingEvent.TotalPrice = updatedEvent.TotalPrice;

            }
            catch (InvalidOperationException)
            {
                CreateRentEvent(companyId, updatedEvent);
            }
        }

        public RentEvent GetActiveRentEventByScooterId(string companyId, string scooterId)
        {
            Company company = companyRepository.GetCompanyById(companyId);
            return company.RentEvents.Single(x => x.ScooterId == scooterId && x.IsActive == true);
        }

        private IList<RentEvent> GetRentalsByYear(string companyId, int? year, bool activeEvents)
        {
            Company company = companyRepository.GetCompanyById(companyId);

            if (year.HasValue)
            {
                return company.RentEvents.Where(x => x.StartDate.Year == year && x.IsActive == activeEvents).ToList();
            }

            return company.RentEvents.Where(x => x.IsActive == false).ToList();

        }

        public IList<RentEvent> GetCompletedRentalsByYear(string companyId, int? year)
        {
            return GetRentalsByYear(companyId, year, false);
        }

        public IList<RentEvent> GetActiveEventsByYear(string companyId, int? year)
        {
            return GetRentalsByYear(companyId, year, true);
        }
    }
}
