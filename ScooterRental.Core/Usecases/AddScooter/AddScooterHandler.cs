using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Core.Usecases.AddScooter
{
    public class AddScooterHandler : IAddScooterHandler
    {
        IScooterService ScooterService;
        IAddScooterValidator AddScooterValidator;

        public AddScooterHandler(IScooterService scooterService, IAddScooterValidator addScooterValidator)
        {
            ScooterService = scooterService;
            AddScooterValidator = addScooterValidator;
        }

        public void Handle(string id, decimal pricePerMinute)
        {
            AddScooterValidator.Validate(id);
            AddScooterValidator.Validate(pricePerMinute);

            ScooterService.AddScooter(id, pricePerMinute);
        }
    }
}
