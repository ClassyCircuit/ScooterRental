using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Infrastructure.Services
{
    public class BusinessLogicRepository : IBusinessLogicRepository
    {
        private readonly ICompanyRepository companyRepository;

        public BusinessLogicRepository(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public PriceLimit GetPriceLimits(string companyId)
        {
            var company = companyRepository.GetCompanyById(companyId);
            return company.PriceLimit;
        }
    }
}
