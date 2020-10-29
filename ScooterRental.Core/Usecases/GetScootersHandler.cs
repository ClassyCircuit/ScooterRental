using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class GetScootersHandler : IGetScootersHandler
    {
        IScooterRepository scooterRepository;

        public GetScootersHandler(IScooterRepository scooterRepository)
        {
            this.scooterRepository = scooterRepository;
        }

        public IList<Scooter> Handle(string companyId)
        {
            IList<Scooter> scooters = scooterRepository.GetScooters(companyId);

            return scooters;
        }
    }
}
