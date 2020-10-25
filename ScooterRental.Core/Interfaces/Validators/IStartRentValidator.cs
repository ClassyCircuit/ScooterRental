using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Validators
{
    public interface IStartRentValidator
    {
        Scooter Validate(string id, string companyId);
    }
}