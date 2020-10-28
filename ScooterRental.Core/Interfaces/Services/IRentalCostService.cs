using ScooterRental.Core.Entities;
using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface IRentalCostService
    {
        IList<RentEvent> Calculate(RentEvent rentEvent, DateTime EndDate);
    }
}