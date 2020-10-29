using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface IBusinessLogicRepository
    {
        PriceLimit GetPriceLimits(string companyId);
    }
}