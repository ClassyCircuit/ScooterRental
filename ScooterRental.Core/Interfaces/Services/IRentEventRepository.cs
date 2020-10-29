using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface IRentEventRepository
    {
        void CreateRentEvent(string companyId, RentEvent rentEvent);
        IList<RentEvent> GetActiveEventsByYear(string companyId, int? year);
        RentEvent GetActiveRentEventByScooterId(string companyId, string scooterId);
        IList<RentEvent> GetCompletedRentalsByYear(string companyId, int? year);
        RentEvent GetRentEventById(string companyId, string rentEventId);
        void UpsertRentEvent(string companyId, RentEvent updatedEvent);
    }
}