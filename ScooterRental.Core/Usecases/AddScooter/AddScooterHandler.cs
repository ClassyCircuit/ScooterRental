using ScooterRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Core.Usecases.AddScooter
{
    public class AddScooterHandler
    {
        IScooterService ScooterService;
        AddScooterValidator AddScooterValidator;

        public AddScooterHandler(IScooterService scooterService, AddScooterValidator addScooterValidator)
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
