using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Infrastructure.Data;
using System.Linq;

namespace ScooterRental.Infrastructure.Services
{
    /// <summary>
    /// Repository persists domain model in-memory.
    /// Company is the Aggregate Root in domain model, so only one repository per aggregate root should exist.
    /// It is possible to split Company, RentEvents and Scooters into 3 aggregate roots with 3 value-objects, but that is unnecessary complication, since there are no entities lower in hierarchy.
    /// Single responsibility principle still applies and logical groups should be in separate internal classes.
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Context context;

        public CompanyRepository(Context context)
        {
            this.context = context;
        }

        public Company GetCompanyByName(string name)
        {
            return context.Company.Single(x => x.Name == name);
        }

        public Company GetCompanyById(string companyId)
        {
            return context.Company.Single(x => x.Id == companyId);
        }
    }
}
