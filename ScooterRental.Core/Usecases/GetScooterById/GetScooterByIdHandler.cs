using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases.GetScooterById
{
    public class GetScooterByIdHandler : IGetScooterByIdHandler
    {
        IScooterService scooterService;
        IGetScooterByIdValidator validator;

        public GetScooterByIdHandler(IScooterService scooterService, IGetScooterByIdValidator getScooterByIdValidator)
        {
            this.scooterService = scooterService;
            validator = getScooterByIdValidator;
        }

        public Scooter Handle(string id)
        {
            validator.Validate(id);

            Scooter scooter = scooterService.GetScooterById(id);

            return scooter;
        }
    }
}
