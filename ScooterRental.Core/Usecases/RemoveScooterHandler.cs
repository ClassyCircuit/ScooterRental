using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using System;

namespace ScooterRental.Core.Usecases
{
    public class RemoveScooterHandler : IRemoveScooterHandler
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IRemoveScooterValidator validator;


        public RemoveScooterHandler(ICompanyRepository companyRepository, IRemoveScooterValidator validator)
        {
            this.companyRepository = companyRepository;
            this.validator = validator;
        }

        public void Handle(string id, string companyId)
        {
            validator.Validate(id, companyId);
            try
            {
                companyRepository.RemoveScooter(companyId, id);
            }
            catch (InvalidOperationException)
            {
                throw new EntityDoesNotExistException($"Scooter with ID: {id} does not exist, so the removal failed.");
            }
        }
    }
}
