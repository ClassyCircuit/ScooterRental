using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System;

namespace ScooterRental.Core.Services
{
    public class CostCalculatorService : ICostCalculatorService
    {
        // TODO: If time < 1 min, return 0 cost

        // TODO: If time > 1 day - costs must be divided per day basis. Create new rent event for each day and close the previous one.
        public decimal GetCostFor(RentEvent rentEvent)
        {
            throw new NotImplementedException();
        }
    }
}
