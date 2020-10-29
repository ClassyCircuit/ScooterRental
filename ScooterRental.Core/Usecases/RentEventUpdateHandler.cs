using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class RentEventUpdateHandler : IRentEventUpdateHandler
    {
        private readonly IRentEventRepository rentEventRepository;

        public RentEventUpdateHandler(IRentEventRepository rentEventRepository)
        {
            this.rentEventRepository = rentEventRepository;
        }

        public void Handle(string companyId, IList<RentEvent> rentEvents)
        {
            foreach (var rentEvent in rentEvents)
            {
                rentEventRepository.UpsertRentEvent(companyId, rentEvent);
            }
        }
    }
}
