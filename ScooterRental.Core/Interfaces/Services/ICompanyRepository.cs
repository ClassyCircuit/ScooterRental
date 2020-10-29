using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    /// <summary>
    /// Interacts with a persistent storage to retrieve company entity.
    /// </summary>
    public interface ICompanyRepository
    {
        Company GetCompanyById(string companyId);
        Company GetCompanyByName(string name);
        void AddCompany(Company company);
    }
}