using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface IScooterRepository
    {
        void AddScooter(string companyId, string id, decimal pricePerMinute);
        Scooter GetScooterById(string companyId, string scooterId);
        IList<Scooter> GetScooters(string companyId);
        void RemoveScooter(string companyId, string id);
        void UpdateScooter(string companyId, Scooter scooter);
    }
}