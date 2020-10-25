using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface ICompanyRepository
    {
        void AddScooter(string companyId, string id, decimal pricePerMinute);
        void CreateRentEvent(string companyId, RentEvent rentEvent);
        Company GetCompanyById(string companyId);
        Company GetCompanyByName(string name);
        RentEvent GetRentEventById(string companyId, string rentEvent);
        Scooter GetScooterById(string companyId, string scooterId);
        IList<Scooter> GetScooters(string companyId);
        void RemoveScooter(string companyId, string id);
        void UpdateRentEvent(string companyId, RentEvent updatedEvent);
        void UpdateScooter(string companyId, Scooter scooter);
        RentEvent GetActiveRentEventByScooterId(string companyId, string scooterId);
    }
}