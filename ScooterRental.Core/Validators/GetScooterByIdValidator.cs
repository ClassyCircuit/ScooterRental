using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Validators
{
    public class GetScooterByIdValidator : IGetScooterByIdValidator
    {
        public void Validate(string id)
        {
            if (id == "")
            {
                throw new IdCannotBeEmptyException("Scooter ID must have a value");
            }
        }
    }
}