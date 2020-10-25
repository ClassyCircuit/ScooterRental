using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Validators
{
    public class GetRentalCompanyValidator : IGetRentalCompanyValidator
    {
        public void Validate(string name)
        {
            if (name == "")
            {
                throw new IdCannotBeEmptyException("Company name cannot be empty.");
            }


        }
    }
}
