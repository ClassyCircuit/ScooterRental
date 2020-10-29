using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public static class Extensions
    {
        public static decimal GetRentEventTotalCosts(this IList<RentEvent> rentEvents)
        {
            decimal sum = 0;
            try
            {

                foreach (var x in rentEvents)
                {
                    sum += x.TotalPrice;
                }

            }
            catch (NullReferenceException)
            {
                throw new MissingRentEventsException("Total cost sum method called on an uninitialized and empty list of rent events.");
            }

            return sum;
        }
    }
}
