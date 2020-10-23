using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace ScooterRental.Infrastructure
{
    public class ScooterService : IScooterService
    {
        private IList<Scooter> Scooters = new List<Scooter>();

        public void AddScooter(string id, decimal pricePerMinute)
        {
            throw new NotImplementedException();
        }

        public Scooter GetScooterById(string scooterId)
        {
            throw new NotImplementedException();
        }

        public IList<Scooter> GetScooters()
        {
            return Scooters;
        }

        public void RemoveScooter(string id)
        {
            throw new NotImplementedException();
        }
    }
}
