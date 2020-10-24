using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases.GetScooterById
{
    public class GetScooterByIdHandler
    {
        IScooterService scooterService;
        GetScooterByIdValidator validator;

        public GetScooterByIdHandler(IScooterService scooterService, GetScooterByIdValidator getScooterByIdValidator)
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
