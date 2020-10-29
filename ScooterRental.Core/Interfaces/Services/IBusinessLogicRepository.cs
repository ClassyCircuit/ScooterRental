using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    /// <summary>
    /// Interacts with a persistent storage to retrieve business logic, such as price limits.
    /// </summary>
    public interface IBusinessLogicRepository
    {
        PriceLimit GetPriceLimits(string companyId);
    }
}