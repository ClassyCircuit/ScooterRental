using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces;
using System;

namespace ScooterRental.Core.Usecases.RemoveScooter
{
    public class RemoveScooterValidator
    {
        readonly IScooterService scooterService;

        public RemoveScooterValidator(IScooterService scooterService)
        {
            this.scooterService = scooterService;
        }

        public void Validate(string id)
        {
            if (id == "")
            {
                throw new IdCannotBeEmptyException("Scooter ID must have a value");
            }

            var scooter = scooterService.GetScooterById(id);
            if (scooter.IsRented)
            {
                throw new RentedScooterCannotBeRemovedException($"Scooter with ID: {id} is currently rented, so it cannot be removed.");
            }
        }
    }
}