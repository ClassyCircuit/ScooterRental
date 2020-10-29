using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    /// <summary>
    /// Interacts with a persistent storage for CRUD operations on RentEvent entities.
    /// </summary>
    public interface IRentEventRepository
    {
        /// <summary>
        /// Saves a new rent event.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="rentEvent"></param>
        void CreateRentEvent(string companyId, RentEvent rentEvent);

        /// <summary>
        /// Gets all active rent events, optionally filtered by year.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        IList<RentEvent> GetActiveEventsByYear(string companyId, int? year);

        /// <summary>
        /// Gets all active rent events for a particular scooter.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="scooterId"></param>
        /// <returns></returns>
        RentEvent GetActiveRentEventByScooterId(string companyId, string scooterId);

        /// <summary>
        /// Gets all completed rent events, optionally filtered by year.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        IList<RentEvent> GetCompletedRentalsByYear(string companyId, int? year);

        /// <summary>
        /// Get a single rent event by its id.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="rentEventId"></param>
        /// <returns></returns>
        RentEvent GetRentEventById(string companyId, string rentEventId);

        /// <summary>
        /// Add or Update a rent event.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="updatedEvent"></param>
        void UpsertRentEvent(string companyId, RentEvent updatedEvent);
    }
}