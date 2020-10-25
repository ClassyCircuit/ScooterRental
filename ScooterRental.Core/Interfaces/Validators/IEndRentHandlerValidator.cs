using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Validators
{
    public interface IEndRentHandlerValidator
    {
        void Validate(Scooter scooter);
    }
}