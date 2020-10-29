using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Services
{
    public interface ICompanyRepository
    {
        Company GetCompanyById(string companyId);
        Company GetCompanyByName(string name);
    }
}