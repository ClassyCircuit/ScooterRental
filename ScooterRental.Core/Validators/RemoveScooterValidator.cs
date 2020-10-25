using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Validators
{
    public class RemoveScooterValidator : IRemoveScooterValidator
    {
        readonly IGetScooterByIdHandler getScooterByIdHandler;

        public RemoveScooterValidator(IGetScooterByIdHandler getScooterByIdHandler)
        {
            this.getScooterByIdHandler = getScooterByIdHandler;
        }

        public void Validate(string id, string companyId)
        {
            var scooter = getScooterByIdHandler.Handle(id, companyId);
            if (scooter.IsRented)
            {
                throw new RentedScooterCannotBeRemovedException($"Scooter with ID: {id} is currently rented, so it cannot be removed.");
            }
        }
    }
}