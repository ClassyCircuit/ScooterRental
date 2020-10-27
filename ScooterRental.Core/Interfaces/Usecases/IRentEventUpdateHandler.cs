using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IRentEventUpdateHandler
    {
        void Handle(string companyId, IList<RentEvent> rentEvents);
    }
}