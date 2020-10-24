using ScooterRental.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScooterRental.Core.Interfaces
{
    public interface IScooterService
    {
        /// <summary>
        /// Add scooter.
        /// </summary>
        /// <param name="id">Unique ID of the scooter.</param>
        /// <param name="pricePerMinute">Rental price of the scooter per one minute.</param>
        void AddScooter(string id, decimal pricePerMinute);
        // TODO: factory that creates new scooters

        /// <summary>
        /// Remove scooter. This action is not allowed for scooters if the rental is in progress.
        /// </summary>
        /// <param name="id">Unique ID of the scooter.</param>
        void RemoveScooter(string id);
        // TODO: Business logic -> check if IsRented = true

        /// <summary>
        /// List of scooters that belong to the company.
        /// </summary>
        /// <returns>Return a list of available scooters.</returns>
        IList<Scooter> GetScooters();

        /// <summary>
        /// Get particular scooter by ID.
        /// </summary>
        /// <param name="scooterId">Unique ID of the scooter.</param>
        /// <returns>Return a particular scooter.</returns>
        Scooter GetScooterById(string scooterId);
    }
}
