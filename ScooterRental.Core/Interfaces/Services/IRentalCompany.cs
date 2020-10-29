namespace ScooterRental.Core.Interfaces.Services
{
    public interface IRentalCompany
    {
        /// <summary>
        /// Name of the company.
        /// </summary>
        string Name { get; }
        // TODO: Ask question - should it be possible to create multiple rental companies at the same time?
        /// <summary>
        /// Start the rent of the scooter.
        /// </summary>
        /// <param name="id">ID of the scooter.</param>
        void StartRent(string id);
        // TODO: Ask question - is the 20 eur limit per scooter or per person?
        // TODO: Ask question - can multiple scooters be rented at the same time by the same person?

        /// <summary>
        /// End the rent of the scooter.
        /// </summary>
        /// <param name="id">ID of the scooter.</param>
        /// <returns>The total price of rental. It has to calculated taking into account for how long time scooter was rented.
        /// If total amount per day reaches 20 EUR than timer must be stopped and restarted at beginning of next day at 0:00 am.</returns>
        decimal EndRent(string id);
        // TODO: Ask question - where to report income if rent is spread over two months or two years. 31.12 - 01.01. And 31.10 - 05.11.
        // TODO: Ask question - what if the person rents the scooter for less than 1 minute? Should they be charged?

        /// <summary>
        /// Income report.
        /// </summary>
        /// <param name="year">Year of the report. Sum all years if value is not set.</param>
        /// <param name="includeNotCompletedRentals">Include income from the scooters that are rented out (rental has not ended yet) and
        /// calculate rental
        /// price as if the rental would end at the time when this report was requested.</param>
        /// <returns>The total price of all rentals filtered by year if given.</returns>
        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}
