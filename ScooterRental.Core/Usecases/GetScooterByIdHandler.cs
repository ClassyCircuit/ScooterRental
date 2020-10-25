using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases
{
    public class GetScooterByIdHandler : IGetScooterByIdHandler
    {
        ICompanyRepository companyRepository;
        IGetScooterByIdValidator validator;

        public GetScooterByIdHandler(ICompanyRepository companyRepository, IGetScooterByIdValidator getScooterByIdValidator)
        {
            this.companyRepository = companyRepository;
            validator = getScooterByIdValidator;
        }

        public Scooter Handle(string id, string companyId)
        {
            validator.Validate(id);

            Scooter scooter = companyRepository.GetScooterById(companyId, id);

            if (scooter == null)
            {
                throw new EntityDoesNotExistException($"Scooter with id: {id} does not exist.");
            }

            return scooter;
        }
    }
}
