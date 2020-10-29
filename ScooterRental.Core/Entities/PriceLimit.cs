namespace ScooterRental.Core.Entities
{
    /// <summary>
    /// Holds business logic for price limits.
    /// </summary>
    public class PriceLimit
    {
        public PriceLimit(string id, decimal costLimitPerDay, Company company)
        {
            Id = id;
            CostLimitPerDay = costLimitPerDay;
            Company = company;
        }

        /// <summary>
        /// Unique identifier;
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Maximum amount of money that can be charged for a customer per scooter.
        /// Read from an external source.
        /// </summary>
        public decimal CostLimitPerDay { get; set; }

        public Company Company { get; set; }

    }
}
