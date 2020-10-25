using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Validators
{
    public class StartRentValidator : IStartRentValidator
    {
        readonly IGetScooterByIdHandler getScooterByIdHandler;

        public StartRentValidator(IGetScooterByIdHandler getScooterByIdHandler)
        {
            this.getScooterByIdHandler = getScooterByIdHandler;
        }

        public Scooter Validate(string id, string companyId)
        {
            var scooter = getScooterByIdHandler.Handle(id, companyId);
            if (scooter.IsRented)
            {
                throw new ScooterIsAlreadyBeingRentedException($"Scooter with ID: {id} is already being rented, so a new rent cannot start at this time.");
            }

            return scooter;
        }
    }
}
