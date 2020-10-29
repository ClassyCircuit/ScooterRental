using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    /// <summary>
    /// Interacts with a persistent storage to retrieve business logic, such as price limits.
    /// </summary>
    public interface IBusinessLogicRepository
    {
        /// <summary>
        /// Retrives price limits.
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        PriceLimit GetPriceLimits(string companyId);

        /// <summary>
        /// Updates price limit for a company.
        /// </summary>
        /// <param name="priceLimit"></param>
        void UpdatePriceLimit(PriceLimit priceLimit, string companyId);
    }
}