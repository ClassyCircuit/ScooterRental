using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Validators
{
    /// <summary>
    /// Validator for end rent usecase.
    /// </summary>
    public interface IEndRentValidator
    {
        void Validate(Scooter scooter);
    }
}