using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    /// <summary>
    /// Interacts with a persistent storage for CRUD operations on scooter entity. 
    /// </summary>
    public interface IScooterRepository
    {
        /// <summary>
        /// Saves a new scooter.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="id"></param>
        /// <param name="pricePerMinute"></param>
        void AddScooter(string companyId, string id, decimal pricePerMinute);

        /// <summary>
        /// Gets a particular scooter by its id.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="scooterId"></param>
        /// <returns></returns>
        Scooter GetScooterById(string companyId, string scooterId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        IList<Scooter> GetScooters(string companyId);
        void RemoveScooter(string companyId, string id);
        void UpdateScooter(string companyId, Scooter scooter);
    }
}