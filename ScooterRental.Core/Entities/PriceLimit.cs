namespace ScooterRental.Core.Entities
{
    /// <summary>
    /// Holds business logic for price limits.
    /// </summary>
    public class PriceLimit
    {
        /// <summary>
        /// Maximum amount of money that can be charged for a customer per scooter.
        /// Read from an external source.
        /// </summary>
        public decimal CostLimitPerDay;
    }
}
