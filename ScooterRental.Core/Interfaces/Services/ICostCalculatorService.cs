using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface ICostCalculatorService
    {
        IList<RentEvent> CalculateRentEventCosts(RentEvent rentEvent);
        decimal GetRentEventTotalCost(IList<RentEvent> rentEvents);
    }
}