using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Infrastructure
{
    public class ScooterService : IScooterService
    {
        private readonly IList<Scooter> scooters;

        public ScooterService(IList<Scooter> scooters)
        {
            this.scooters = scooters;
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            scooters.Add(new Scooter(id, pricePerMinute));
        }

        public Scooter GetScooterById(string scooterId)
        {
            return scooters.FirstOrDefault(x => x.Id == scooterId);
        }

        public IList<Scooter> GetScooters()
        {
            return scooters;
        }

        public void RemoveScooter(string id)
        {
            scooters.Remove(scooters.FirstOrDefault(x => x.Id == id));
        }
    }
}
