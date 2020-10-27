using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class RentEventUpdateHandler : IRentEventUpdateHandler
    {
        private readonly ICompanyRepository companyRepository;

        public RentEventUpdateHandler(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void Handle(string companyId, IList<RentEvent> rentEvents)
        {
            foreach (var rentEvent in rentEvents)
            {
                companyRepository.UpsertRentEvent(companyId, rentEvent);
            }
        }
    }
}
