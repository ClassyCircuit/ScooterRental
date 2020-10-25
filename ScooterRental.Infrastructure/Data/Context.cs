using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Infrastructure.Data
{
    /// <summary>
    /// Holds application data.
    /// </summary>
    public class Context
    {
        public Context(IList<RentEvent> rentEvents, IList<Scooter> scooters, IList<Company> company)
        {
            RentEvents = rentEvents;
            Scooters = scooters;
            Company = company;
        }

        public IList<Company> Company { get; set; }
        public IList<RentEvent> RentEvents { get; set; }
        public IList<Scooter> Scooters { get; set; }
    }
}
