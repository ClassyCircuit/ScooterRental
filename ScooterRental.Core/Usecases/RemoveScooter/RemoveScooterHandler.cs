using ScooterRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Core.Usecases.RemoveScooter
{
    public class RemoveScooterHandler
    {
        readonly IScooterService scooterService;
        readonly RemoveScooterValidator validator;

        public RemoveScooterHandler(IScooterService scooterService, RemoveScooterValidator validator)
        {
            this.scooterService = scooterService;
            this.validator = validator;
        }

        public void RemoveScooter(string id)
        {
            validator.Validate(id);
            scooterService.RemoveScooter(id);
        }
    }
}
