using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases.GetScooters
{
    public class GetScootersHandler : IGetScootersHandler
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
