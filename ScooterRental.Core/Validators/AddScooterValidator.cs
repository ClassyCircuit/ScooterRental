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
            try
            {
                var result = getScooterByIdHandler.Handle(id, companyId);
                if (result != null)
                {
                    throw new IdNotUniqueException("Scooter ID already exists.");
                }

            }
            catch (EntityDoesNotExistException)
            {
                // If entity is not found in this case then it is a successful business scenario.
                return;
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
