using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface IRentEventService
    {
        void CreateEvent(RentEvent rentEvent);
    }
}
