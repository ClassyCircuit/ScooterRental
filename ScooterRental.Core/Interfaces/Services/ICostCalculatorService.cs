using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface ICostCalculatorService
    {
        decimal GetCostFor(RentEvent rentEvent);
    }
}