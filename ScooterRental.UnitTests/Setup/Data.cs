using ScooterRental.Core.Entities;
using ScooterRental.Core.Services.Builders;
using System.Collections.Generic;

namespace ScooterRental.UnitTests.Setup
{
    /// <summary>
    /// Holds re-usable mock objects for setting up unit tests.
    /// </summary>
    public class Data
    {
        public Company Company { get; }
        public IList<Scooter> Scooters { get; }
        public IList<RentEvent> RentEvents { get; }
        public string ExistingScooterId { get; }
        public PriceLimit PriceLimit { get; internal set; }

        public Data()
        {
            Company = CompanyBuilder.Default().Build();
            PriceLimit = PriceLimitBuilder.Default(Company).Build();

            Scooters = new List<Scooter>()
            {
                ScooterBuilder.Default(Company).Build(),
                ScooterBuilder.Default(Company).Build(),
                ScooterBuilder.Default(Company).Build(),
                ScooterBuilder.Default(Company).Build(),
                ScooterBuilder.Default(Company).Build(),
            };

            RentEvents = new List<RentEvent>()
            {
                RentEventBuilder.Default(Company, Scooters[0]).WithTotalPrice(GetRandom.Decimal(0,5)).Build(),
                RentEventBuilder.Default(Company, Scooters[1]).WithTotalPrice(GetRandom.Decimal(0,5)).Build(),
                RentEventBuilder.Default(Company, Scooters[0]).WithTotalPrice(GetRandom.Decimal(0,5)).Build(),
            };

            Company.RentEvents = RentEvents;
            Company.Scooters = Scooters;

            ExistingScooterId = Scooters[0].Id;

        }
    }
}
