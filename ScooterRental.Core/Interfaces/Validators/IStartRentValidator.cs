using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Validators
{
    /// <summary>
    /// Validator for StartRent usecase.
    /// </summary>
    public interface IStartRentValidator
    {
        Scooter Validate(string id, string companyId);
    }
}