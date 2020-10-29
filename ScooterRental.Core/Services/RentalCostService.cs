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
        public decimal CostLimitPerDay { get; }

        public RentalCostService(decimal costLimitPerDay)
        {
            CostLimitPerDay = costLimitPerDay;
        }

        /// <summary>
        /// Calculates total cost for a rent event.
        /// If it spans multiple days, then it is split into multiple rent events.
        /// </summary>
        /// <param name="rentEvent">Rent event for which to calculate costs</param>
        /// <returns></returns>
        public IList<RentEvent> Calculate(RentEvent rentEvent, DateTime endDate)
        {
            DateTime currentDay = rentEvent.StartDate;
            IList<RentEvent> updatedRentEvents = new List<RentEvent>();

            while (currentDay <= endDate.Date)
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

        private static RentEvent CreateNewEvent(RentEvent rentEvent, DateTime currentDayAtMidnight)
        {
            return new RentEvent(currentDayAtMidnight,
                                null,
                                rentEvent.PricePerMinute,
                                rentEvent.IsActive,
                                Guid.NewGuid().ToString(),
                                rentEvent.Company,
                                rentEvent.ScooterId);
        }

        private RentEvent UpdateRentEvent(RentEvent rentEvent, DateTime startDate, DateTime endDate)
        {
            // How many mins till cost limit
            int minutesTillCostLimit = (int)Math.Floor(CostLimitPerDay / rentEvent.PricePerMinute);
            DateTime ActualEndDate = endDate;
            int minutesLeftInDay = GetMinutesLeftInDay(rentEvent);
            TimeSpan timeLeftInDay = GetTimeLeftInDay(startDate);

            TimeSpan rentDuration = endDate - startDate;

            // If remaining rent period spills over to the next day, then calculate cost only for remaining time in the current day.
            if (timeLeftInDay < rentDuration)
            {
                rentDuration = timeLeftInDay;
                ActualEndDate = startDate + timeLeftInDay;
            }

            decimal totalCost = Math.Round(Convert.ToDecimal(rentDuration.TotalMinutes) * rentEvent.PricePerMinute, 2);

            if (totalCost > CostLimitPerDay)
            {
                totalCost = CostLimitPerDay;
                ActualEndDate = startDate.AddMinutes(minutesTillCostLimit);
            }

            // If total rent period is less than one minute, don't charge anything.
            if (totalCost < rentEvent.PricePerMinute)
            {
                totalCost = 0m;
            }

            rentEvent.TotalPrice = totalCost;
            rentEvent.EndDate = ActualEndDate;

            return rentEvent;
        }

        private TimeSpan GetTimeLeftInDay(DateTime StartDate)
        {
            return StartDate.Date.AddDays(1).AddTicks(-1) - StartDate;
        }

        private static int GetMinutesLeftInDay(RentEvent rentEvent)
        {
            DateTime endOfDay = rentEvent.StartDate.Date.AddDays(1);
            return (int)Math.Floor((endOfDay - rentEvent.StartDate).TotalMinutes);
        }

    }
}
