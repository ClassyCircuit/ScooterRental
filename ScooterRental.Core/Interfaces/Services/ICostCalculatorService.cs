using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface ICostCalculatorService
    {
        IList<RentEvent> GetRentEventCosts(RentEvent rentEvent)
    }
}