using System.Collections.Generic;

namespace ScooterRental.Core.Entities
{
    /// <summary>
    /// Aggregate root of domain entities. Changes to lower hierarchy must always be in sync with the root object.
    /// </summary>
    public class Company
    {
        public Company(string name, string id)
        {
            Scooters = new List<Scooter>();
            RentEvents = new List<RentEvent>();
            Name = name;
            Id = id;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public IList<Scooter> Scooters { get; set; }

        public IList<RentEvent> RentEvents { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
