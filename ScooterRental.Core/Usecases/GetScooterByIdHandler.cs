using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases
{
    public class GetScooterByIdHandler : IGetScooterByIdHandler
    {
        IScooterRepository scooterRepository;
        IGetScooterByIdValidator validator;

        public GetScooterByIdHandler(IScooterRepository scooterRepository, IGetScooterByIdValidator getScooterByIdValidator)
        {
            this.scooterRepository = scooterRepository;
            validator = getScooterByIdValidator;
        }

        public Scooter Handle(string id, string companyId)
        {
            validator.Validate(id);

            Scooter scooter = scooterRepository.GetScooterById(companyId, id);

            if (scooter == null)
            {
                throw new EntityDoesNotExistException($"Scooter with id: {id} does not exist.");
            }

            return scooter;
        }
    }
}
