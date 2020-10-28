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
            IList<RentEvent> updatedRentEvents = new List<RentEvent>();
            int daysOfRent = GetAmountOfDaysInRentalPeriod(rentEvent);

            for (int i = 0; i < daysOfRent; i++)
            {
                if (i > 0)
                {
                    DateTime nextDay = rentEvent.StartDate.AddDays(1).Date;
                    rentEvent = CreateNewEvent(rentEvent, nextDay);
                }

                bool isLastDay = i == (daysOfRent - 1);
                rentEvent = UpdateRentEvent(rentEvent, isLastDay);
                updatedRentEvents.Add(rentEvent);
            }

            return updatedRentEvents;
        }

        private static RentEvent CreateNewEvent(RentEvent rentEvent, DateTime nextDayAtMidnight)
        {
            return new RentEvent(nextDayAtMidnight, null, rentEvent.PricePerMinute, rentEvent.IsActive, Guid.NewGuid().ToString(), rentEvent.Company, rentEvent.ScooterId);
        }

        /// <summary>
        /// Returns how many days the entire rental period spans.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <returns></returns>
        private static int GetAmountOfDaysInRentalPeriod(RentEvent rentEvent)
        {
            TimeSpan rentalTimeSpan = DateTime.UtcNow - rentEvent.StartDate;
            int daysOfRent = Convert.ToInt32(Math.Ceiling(rentalTimeSpan.TotalDays));
            return daysOfRent;
        }

        /// <summary>
        /// Calculates if the cost limit has been reached for this day's RentEvent 
        /// and depending on the result sets the total cost and also the end date.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <returns></returns>
        private RentEvent UpdateRentEvent(RentEvent rentEvent, bool isLastDay)
        {
            decimal minutesLeftInDay = GetMinutesLeftInDay(rentEvent);
            decimal minutesTillCostLimit = CostLimitPerDay / rentEvent.PricePerMinute;

            if (IsCostLimitReached(minutesLeftInDay, minutesTillCostLimit))
            {
                rentEvent = UpdateWhenLimitIsReached(rentEvent, minutesTillCostLimit);
            }
            else
            {
                rentEvent = UpdateWhenLimitIsNotReached(rentEvent, minutesLeftInDay);
            }

            return rentEvent;
        }

        /// <summary>
        /// In case cost limit has been reached this day, then use it to set the total cost
        /// and end date should be however long it took to reach the cost limit.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <param name="minutesTillCostLimit"></param>
        /// <returns></returns>
        private RentEvent UpdateWhenLimitIsReached(RentEvent rentEvent, decimal minutesTillCostLimit)
        {
            rentEvent.TotalPrice = CostLimitPerDay;
            rentEvent.EndDate = rentEvent.StartDate.AddMinutes(Convert.ToDouble(minutesTillCostLimit));

            return rentEvent;
        }

        /// <summary>
        /// If cost limit is not reached, use the actual rental time in that day to calculate the costs and end time.
        /// </summary>
        /// <param name="rentEvent"></param>
        /// <param name="minutesLeftInDay"></param>
        /// <returns></returns>
        private RentEvent UpdateWhenLimitIsNotReached(RentEvent rentEvent, decimal minutesLeftInDay)
        {
            if (minutesLeftInDay < 1)
            {
                rentEvent.TotalPrice = 0;
            }
            else
            {
                rentEvent.TotalPrice = rentEvent.PricePerMinute * minutesLeftInDay;
            }

            rentEvent.EndDate = rentEvent.StartDate.AddMinutes(Convert.ToDouble(minutesLeftInDay));

            return rentEvent;
        }

        private static decimal GetMinutesLeftInDay(RentEvent rentEvent)
        {
            return Convert.ToDecimal((new DateTime(rentEvent.StartDate.Year, rentEvent.StartDate.Month, rentEvent.StartDate.Day, 0, 0, 0) - rentEvent.StartDate).TotalMinutes);
        }

        /// <summary>
        /// If the remaining time in the day is more than the time it takes to reach the maximum money limit, then the limit has been reached.
        /// </summary>
        /// <param name="minutesLeftInDay"></param>
        /// <param name="minutesTillCostLimit"></param>
        /// <returns></returns>
        private bool IsCostLimitReached(decimal minutesLeftInDay, decimal minutesTillCostLimit)
        {
            return (minutesLeftInDay - minutesTillCostLimit) > 0;
        }
    }
}
