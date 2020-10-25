using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Validators
{
    public class EndRentHandlerValidator : IEndRentHandlerValidator
    {
        public void Validate(Scooter scooter)
        {
            if (!scooter.IsRented)
            {
                throw new ScooterIsNotRentedException($"Scooter with ID: {scooter.IsRented} is not currently rented, so rent cannot be stopped for it.");
            }
        }
    }
}
