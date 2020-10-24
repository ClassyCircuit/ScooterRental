using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases.StartRent
{
    public class StartRentValidator : IStartRentValidator
    {
        readonly IGetScooterByIdHandler getScooterByIdHandler;

        public StartRentValidator(IGetScooterByIdHandler getScooterByIdHandler)
        {
            this.getScooterByIdHandler = getScooterByIdHandler;
        }

        public Scooter Validate(string id)
        {
            var scooter = getScooterByIdHandler.Handle(id);
            if (scooter.IsRented)
            {
                throw new ScooterIsAlreadyBeingRentedException($"Scooter with ID: {id} is already being rented, so a new rent cannot start at this time.");
            }

            return scooter;
        }
    }
}
