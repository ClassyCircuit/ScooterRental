using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Infrastructure
{
    public class ScooterService : IScooterService
    {
        // TODO: Make the same approach as rental company - expose this interface to user and call handlers from it.

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

        public void UpdateScooter(Scooter scooter)
        {
            var existingScooter = scooters.FirstOrDefault(x=>x.Id == scooter.Id);
            existingScooter.IsRented = scooter.IsRented;
        }
    }
}
