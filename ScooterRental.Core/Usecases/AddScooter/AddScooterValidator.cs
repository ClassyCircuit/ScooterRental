using System;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Usecases.GetScooterById;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases.AddScooter
{
    public class AddScooterValidator : IAddScooterValidator
    {
        readonly IGetScooterByIdHandler getScooterByIdHandler;

        public AddScooterValidator(IGetScooterByIdHandler getScooterByIdHandler)
        {
            this.getScooterByIdHandler = getScooterByIdHandler;
        }

        public void Validate(string id)
        {
            var result = getScooterByIdHandler.Handle(id);

            if (result != null)
            {
                throw new IdNotUniqueException("Scooter ID already exists.");
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
