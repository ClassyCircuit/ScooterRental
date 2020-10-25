namespace ScooterRental.Core.Entities
{ // TODO: external file for scooter types (price,id) or factory
    public class Scooter
    {
        /// <summary>
        /// Create new instance of the scooter.
        /// </summary>
        /// <param name="id">ID of the scooter.</param>
        /// <param name="pricePerMinute">Rental price of the scooter per one minute.</param>
        public Scooter(string id, decimal pricePerMinute, Company company)
        {
            Id = id;
            PricePerMinute = pricePerMinute;
            Company = company;
        }

        /// <summary>
        /// Unique ID of the scooter.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Rental price of the scooter per one
        /// minute.
        /// </summary>
        public decimal PricePerMinute { get; }

        /// <summary>
        /// Identify if someone is renting this
        /// scooter.
        /// </summary>
        public bool IsRented { get; set; }

        public Company Company { get; set; }
    }
}
