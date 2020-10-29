using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Infrastructure.Data;
using ScooterRental.Infrastructure.Services;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace ScooterRental.UnitTests.Services
{
    public class CompanyRepositoryTests : TestBase
    {
        private Context context;
        private ICompanyRepository repository;

        public CompanyRepositoryTests(Data mocks) : base(mocks)
        {
            List<Company> companies = new List<Company>()
            {
                mocks.Company
            };

            context = new Context(companies);
            repository = new CompanyRepository(context);
        }

        [Fact]
        public void GetCompanyById_ReturnsCompany()
        {
            // Act
            Company company = repository.GetCompanyById(Data.Company.Id);

            // Assert
            company.Id.ShouldBe(Data.Company.Id);
            company.Name.ShouldBe(Data.Company.Name);
        }

        [Fact]
        public void GetCompanyByName_ReturnsName()
        {
            // Act
            Company company = repository.GetCompanyByName(Data.Company.Name);

            // Assert
            company.Id.ShouldBe(Data.Company.Id);
            company.Name.ShouldBe(Data.Company.Name);
        }
    }
}
