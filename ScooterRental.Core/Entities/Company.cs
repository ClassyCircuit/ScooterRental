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

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Company name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Scooters that belong to this company.
        /// </summary>
        public IList<Scooter> Scooters { get; set; }

        /// <summary>
        /// List of active and completed rent events.
        /// </summary>
        public IList<RentEvent> RentEvents { get; set; }

        /// <summary>
        /// Maximum allowed charge per day.
        /// </summary>
        public PriceLimit PriceLimit { get; set; }
    }
}
