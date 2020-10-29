using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public class ScooterService : IScooterService
    {
        private readonly Company company;
        private readonly IAddScooterHandler addScooterHandler;
        private readonly IGetScooterByIdHandler getScooterByIdHandler;
        private readonly IGetScootersHandler getScootersHandler;
        private readonly IRemoveScooterHandler removeScooterHandler;

        public ScooterService(IAddScooterHandler addScooterHandler, IGetScooterByIdHandler getScooterByIdHandler, IGetScootersHandler getScootersHandler, IRemoveScooterHandler removeScooterHandler, Company company)
        {
            this.addScooterHandler = addScooterHandler;
            this.getScooterByIdHandler = getScooterByIdHandler;
            this.getScootersHandler = getScootersHandler;
            this.removeScooterHandler = removeScooterHandler;
            this.company = company;
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            addScooterHandler.Handle(id, pricePerMinute, company.Id);
        }

        public Scooter GetScooterById(string scooterId)
        {
            return getScooterByIdHandler.Handle(scooterId, company.Id);
        }

        public IList<Scooter> GetScooters()
        {
            return getScootersHandler.Handle(company.Id);
        }

        public void RemoveScooter(string id)
        {
            removeScooterHandler.Handle(id, company.Id);
        }
    }
}
