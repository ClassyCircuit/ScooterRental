using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Validators
{
    public class AddScooterValidator : IAddScooterValidator
    {
        readonly IGetScooterByIdHandler getScooterByIdHandler;

        public AddScooterValidator(IGetScooterByIdHandler getScooterByIdHandler)
        {
            this.getScooterByIdHandler = getScooterByIdHandler;
        }

        public void Validate(string id, string companyId)
        {
            var result = getScooterByIdHandler.Handle(id, companyId);

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
