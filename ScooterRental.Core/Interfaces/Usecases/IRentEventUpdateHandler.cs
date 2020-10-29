using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for updating rental events.
    /// </summary>
    public interface IRentEventUpdateHandler
    {
        void Handle(string companyId, IList<RentEvent> rentEvents);
    }
}