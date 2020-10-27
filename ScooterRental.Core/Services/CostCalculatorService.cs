using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public class CostCalculatorService : ICostCalculatorService
    {
        /// <summary>
        /// Maximum money that is allowed to be charged for a rental per day.
        /// </summary>
        public decimal CostLimitPerDay { get; } = 20m;

        /// <summary>
        /// Calculates total cost for a rent event.
        /// If it spans multiple days, then it is split into multiple rent events.
        /// </summary>
        /// <param name="rentEvent">Rent event for which to calculate costs</param>
        /// <returns></returns>
        public IList<RentEvent> CalculateRentEventCosts(RentEvent rentEvent)
        {
            IList<RentEvent> rentEvents = new List<RentEvent>();
            decimal minutesOfRent = Convert.ToDecimal((DateTime.UtcNow - rentEvent.StartDate).TotalMinutes);

            decimal minutesTillCostLimit = CostLimitPerDay / rentEvent.PricePerMinute;

            if (minutesOfRent < 1)
            {
                rentEvent.TotalPrice = 0;
            }
            else if (minutesOfRent <= minutesTillCostLimit)
            {
                rentEvent.TotalPrice = rentEvent.PricePerMinute * Convert.ToDecimal(minutesOfRent);
            }
            else
            {
                rentEvent.TotalPrice = CostLimitPerDay;
                minutesTillCostLimit -= CostLimitPerDay;

            }


            // return list of updated rent events

            return rentEvents;
        }

        /// <summary>
        /// Sums up total cost for a list of rent events and returns it.
        /// </summary>
        /// <param name="rentEvents">list of rent events with already calculated costs.</param>
        /// <returns></returns>
        public decimal GetRentEventTotalCost(IList<RentEvent> rentEvents)
        {
            decimal sum = 0;
            foreach (var x in rentEvents)
            {
                sum += x.TotalPrice;
            }

            return sum;
        }
    }
}
