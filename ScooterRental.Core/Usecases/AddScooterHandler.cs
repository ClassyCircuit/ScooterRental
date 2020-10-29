using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases
{
    public class AddScooterHandler : IAddScooterHandler
    {
        IScooterRepository scooterRepository;
        IAddScooterValidator addScooterValidator;

        public AddScooterHandler(IScooterRepository scooterRepository, IAddScooterValidator addScooterValidator)
        {
            this.scooterRepository = scooterRepository;
            this.addScooterValidator = addScooterValidator;
        }

        public void Handle(string id, decimal pricePerMinute, string companyId)
        {
            // Validate
            addScooterValidator.Validate(id, companyId);
            addScooterValidator.Validate(pricePerMinute);

            // Add new scooter
            scooterRepository.AddScooter(companyId, id, pricePerMinute);
        }
    }
}
