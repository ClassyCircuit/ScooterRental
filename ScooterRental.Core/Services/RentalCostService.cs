using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public class RentalCostService : IRentalCostService
    {
        /// <summary>
        /// Maximum money that is allowed to be charged for a rental per day.
        /// </summary>
        private PriceLimit priceLimit;

        /// <summary>
        /// Calculates total cost for a rent event.
        /// If it spans multiple days, then it is split into multiple rent events.
        /// </summary>
        /// <param name="rentEvent">Rent event for which to calculate costs</param>
        /// <returns></returns>
        public IList<RentEvent> Calculate(RentEvent rentEvent, DateTime endDate, PriceLimit priceLimit)
        {
            DateTime currentDay = rentEvent.StartDate;
            IList<RentEvent> updatedRentEvents = new List<RentEvent>();
            this.priceLimit = priceLimit;

            while (currentDay.Date <= endDate.Date)
            {
                // For subsequent days new RentEvents are created
                if (currentDay > rentEvent.StartDate)
                {
                    rentEvent = CreateNewEvent(rentEvent, currentDay.Date);
                }

                // For the first day, the existing event is updated
                updatedRentEvents.Add(UpdateRentEvent(rentEvent, currentDay, endDate));
                currentDay = rentEvent.StartDate.AddDays(1).Date;
            }

            return updatedRentEvents;
        }

        /// <summary>
        /// Creates a new instance of rent event.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <param name="currentDayAtMidnight"></param>
        /// <returns></returns>
        private static RentEvent CreateNewEvent(RentEvent rentEvent, DateTime currentDayAtMidnight)
        {
            return new RentEvent(currentDayAtMidnight,
                                null,
                                rentEvent.PricePerMinute,
                                false,
                                Guid.NewGuid().ToString(),
                                rentEvent.Company,
                                rentEvent.ScooterId);
        }

        /// <summary>
        /// Update an existing rent event with the total cost and end date.
        /// Returns the updated event.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private RentEvent UpdateRentEvent(RentEvent rentEvent, DateTime startDate, DateTime endDate)
        {
            // How many mins till cost limit
            int minutesTillCostLimit = (int)Math.Floor(priceLimit.CostLimitPerDay / rentEvent.PricePerMinute);
            DateTime ActualEndDate = endDate;
            TimeSpan timeLeftInDay = GetTimeLeftInDay(startDate);

            TimeSpan rentDuration = endDate - startDate;

            // If remaining rent period spills over to the next day, then calculate cost only for remaining time in the current day.
            if (timeLeftInDay < rentDuration)
            {
                rentDuration = timeLeftInDay;
                ActualEndDate = startDate + timeLeftInDay;
            }

            decimal totalCost = CalculateTotalCost(rentEvent, startDate, minutesTillCostLimit, ref ActualEndDate, rentDuration);

            rentEvent.TotalPrice = totalCost;
            rentEvent.EndDate = ActualEndDate;
            rentEvent.IsActive = false;

            return rentEvent;
        }

        /// <summary>
        /// Calculates total cost for all possible scenarios.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <param name="startDate"></param>
        /// <param name="minutesTillCostLimit"></param>
        /// <param name="ActualEndDate"></param>
        /// <param name="rentDuration"></param>
        /// <returns>Total cost</returns>
        private decimal CalculateTotalCost(RentEvent rentEvent, DateTime startDate, int minutesTillCostLimit, ref DateTime ActualEndDate, TimeSpan rentDuration)
        {
            decimal totalCost = Math.Round(Convert.ToDecimal(rentDuration.TotalMinutes) * rentEvent.PricePerMinute, 2);

            if (totalCost > priceLimit.CostLimitPerDay)
            {
                totalCost = priceLimit.CostLimitPerDay;
                ActualEndDate = startDate.AddMinutes(minutesTillCostLimit);
            }

            // If total rent period is less than one minute, don't charge anything.
            if (totalCost < rentEvent.PricePerMinute)
            {
                totalCost = 0m;
            }

            return totalCost;
        }

        /// <summary>
        /// Calculates how many minutes are left till midnight.
        /// </summary>
        /// <param name="StartDate"></param>
        /// <returns></returns>
        private TimeSpan GetTimeLeftInDay(DateTime StartDate)
        {
            return StartDate.Date.AddDays(1).AddTicks(-1) - StartDate;
        }
    }
}
