using ScooterRental.Core.Interfaces;
using ScooterRental.Core.Interfaces.Usecases;

namespace ScooterRental.Core.Usecases.StartRent
{
    public class StartRentHandler : IStartRentHandler
    {
        readonly IRentalCompany rentalCompany;
        readonly StartRentValidator validator;

        public StartRentHandler(IRentalCompany rentalCompany, StartRentValidator validator)
        {
            this.rentalCompany = rentalCompany;
            this.validator = validator;
        }

        public void Handle(string id)
        {
            validator.Validate(id);
            rentalCompany.StartRent(id);
        }
    }
}
