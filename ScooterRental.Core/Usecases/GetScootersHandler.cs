using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System.Collections.Generic;

namespace ScooterRental.Core.Usecases
{
    public class GetScootersHandler : IGetScootersHandler
    {
        ICompanyRepository companyRepository;

        public GetScootersHandler(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public IList<Scooter> Handle(string companyId)
        {
            IList<Scooter> scooters = companyRepository.GetScooters(companyId);

            return scooters;
        }
    }
}
