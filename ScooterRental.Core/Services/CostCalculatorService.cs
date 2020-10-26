using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public class CostCalculatorService : ICostCalculatorService
    {
        // TODO: If time < 1 min, return 0 cost

        // TODO: If time > 1 day - costs must be divided per day basis. Create new rent event for each day and close the previous one.
        /// <summary>
        /// Calculates total cost for a rent event.
        /// If it spans multiple days, then it is split into multiple rent events.
        /// </summary>
        /// <param name="rentEvent">Rent event for which to calculate costs</param>
        /// <returns></returns>
        public IList<RentEvent> GetRentEventCosts(RentEvent rentEvent)
        {
            IList<RentEvent> rentEvents = new List<RentEvent>();
            // calculate rental time
            rentEvent.EndDate = DateTime.UtcNow;
            TimeSpan duration = rentEvent.EndDate.Value - rentEvent.StartDate;
            rentEvents.Add(rentEvent);
            // if time < 1 min, return 0 cost

            // if time > 1 day, split into multiple events

            // return list of updated rent events

            return rentEvents;
        }
    }
}
