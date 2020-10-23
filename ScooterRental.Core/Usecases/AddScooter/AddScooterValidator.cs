using ScooterRental.Core.Interfaces;
using System;
using ScooterRental.Core.Exceptions;

namespace ScooterRental.Core.Usecases.AddScooter
{
    public class AddScooterValidator
    {
        IScooterService ScooterService;

        public AddScooterValidator(IScooterService scooterService)
        {
            ScooterService = scooterService;
        }

        public void Validate(string id)
        {
            var result = ScooterService.GetScooterById(id);
            if (result != null)
            {
                throw new IdNotUniqueException("Scooter ID already exists.");
            }

            if(id == "")
            {
                throw new IdCannotBeEmptyException("Scooter ID must have a value");
            }
        }

        public void Validate(decimal pricePerMinute)
        {
            if (pricePerMinute <= 0)
            {
                throw new PriceCannotBeNegativeException("Scooter price must be positive.");
            }
        }
    }
}
