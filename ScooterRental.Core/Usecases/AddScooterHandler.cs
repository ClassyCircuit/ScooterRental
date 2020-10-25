using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases
{
    public class AddScooterHandler : IAddScooterHandler
    {
        ICompanyRepository CompanyRepository;
        IAddScooterValidator AddScooterValidator;

        public AddScooterHandler(ICompanyRepository companyRepository, IAddScooterValidator addScooterValidator)
        {
            CompanyRepository = companyRepository;
            AddScooterValidator = addScooterValidator;
        }

        public void Handle(string id, decimal pricePerMinute, string companyId)
        {
            AddScooterValidator.Validate(id, companyId);
            AddScooterValidator.Validate(pricePerMinute);

            CompanyRepository.AddScooter(companyId, id, pricePerMinute);
        }
    }
}
