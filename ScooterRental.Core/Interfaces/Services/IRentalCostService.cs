using ScooterRental.Core.Entities;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    /// <summary>
    /// Service for splitting rental period into multiple events and calculating costs.
    /// </summary>
    public interface IRentalCostService
    {
        /// <summary>
        /// Calculates cost for a rental period.
        /// </summary>
        /// <param name="rentEvent">An active rental event.</param>
        /// <param name="endDate">Date to take into account for calculating total costs.</param>
        /// <param name="priceLimit">Maximum charge allowed for a person per scooter in a single day.</param>
        /// <returns></returns>
        IList<RentEvent> Calculate(RentEvent rentEvent, DateTime endDate, PriceLimit priceLimit);
    }
}