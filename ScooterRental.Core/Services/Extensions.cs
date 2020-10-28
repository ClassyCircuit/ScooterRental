using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public static class Extensions
    {
        public static decimal GetRentEventTotalCosts(this IList<RentEvent> rentEvents)
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
