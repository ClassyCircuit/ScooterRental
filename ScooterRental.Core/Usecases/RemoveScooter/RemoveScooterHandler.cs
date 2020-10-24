using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;

namespace ScooterRental.Core.Usecases.RemoveScooter
{
    public class RemoveScooterHandler : IRemoveScooterHandler
    {
        readonly IScooterService scooterService;
        readonly IRemoveScooterValidator validator;

        public RemoveScooterHandler(IScooterService scooterService, IRemoveScooterValidator validator)
        {
            this.scooterService = scooterService;
            this.validator = validator;
        }

        public void Handle(string id)
        {
            validator.Validate(id);
            scooterService.RemoveScooter(id);
        }
    }
}
