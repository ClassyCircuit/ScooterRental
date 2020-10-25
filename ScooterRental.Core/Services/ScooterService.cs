using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public class ScooterService : IScooterService
    {
        private readonly Company company;
        private readonly AddScooterHandler addScooterHandler;
        private readonly GetScooterByIdHandler getScooterByIdHandler;
        private readonly GetScootersHandler getScootersHandler;
        private readonly RemoveScooterHandler removeScooterHandler;

        public ScooterService(AddScooterHandler addScooterHandler, GetScooterByIdHandler getScooterByIdHandler, GetScootersHandler getScootersHandler, RemoveScooterHandler removeScooterHandler, Company company)
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
