using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Infrastructure.Services
{
    public class RentEventService : IRentEventService
    {
        private IList<RentEvent> rentEvents;

        public RentEventService(IList<RentEvent> rentEvents)
        {
            this.rentEvents = rentEvents;
        }

        public void CreateEvent(RentEvent rentEvent)
        {
            rentEvents.Add(rentEvent);
        }
    }
}
