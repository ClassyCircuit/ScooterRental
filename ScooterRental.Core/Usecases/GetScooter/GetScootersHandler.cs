using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class GetScootersHandler
    {
        IScooterService scooterService;

        public GetScootersHandler(IScooterService scooterService)
        {
            this.scooterService = scooterService;
        }

        public IList<Scooter> Handle()
        {
            IList<Scooter> scooters = scooterService.GetScooters();

            return scooters;
        }
    }
}
