using ScooterRental.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface IRentEventService
    {
        void CreateEvent(RentEvent rentEvent);
    }
}
