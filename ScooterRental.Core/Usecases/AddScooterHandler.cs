using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases
{
    public class AddScooterHandler : IAddScooterHandler
    {
        IScooterRepository scooterRepository;
        IAddScooterValidator AddScooterValidator;

        public AddScooterHandler(IScooterRepository companyRepository, IAddScooterValidator addScooterValidator)
        {
            scooterRepository = companyRepository;
            AddScooterValidator = addScooterValidator;
        }

        public void Handle(string id, decimal pricePerMinute, string companyId)
        {
            AddScooterValidator.Validate(id, companyId);
            AddScooterValidator.Validate(pricePerMinute);

            scooterRepository.AddScooter(companyId, id, pricePerMinute);
        }
    }
}
