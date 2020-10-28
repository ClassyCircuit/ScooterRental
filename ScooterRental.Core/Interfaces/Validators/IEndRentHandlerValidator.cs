using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Validators
{
    public interface IEndRentValidator
    {
        void Validate(Scooter scooter);
    }
}