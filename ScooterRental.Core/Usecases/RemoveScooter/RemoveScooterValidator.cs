using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases.RemoveScooter
{
    public class RemoveScooterValidator : IRemoveScooterValidator
    {
        readonly IGetScooterByIdHandler getScooterByIdHandler;

        public RemoveScooterValidator(IGetScooterByIdHandler getScooterByIdHandler)
        {
            this.getScooterByIdHandler = getScooterByIdHandler;
        }

        public void Validate(string id)
        {
            var scooter = getScooterByIdHandler.Handle(id);
            if (scooter.IsRented)
            {
                throw new RentedScooterCannotBeRemovedException($"Scooter with ID: {id} is currently rented, so it cannot be removed.");
            }
        }
    }
}