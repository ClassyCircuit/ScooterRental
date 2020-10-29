using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Services.Builders
{
    /// <summary>
    /// Builder for creating new Company entities.
    /// </summary>
    public class CompanyBuilder
    {
        private Company company;

        private string id;
        private string name;

        public Company Build()
        {
            if (company == null)
            {
                company = new Company(name, id);
            }

            return company;
        }

        public static CompanyBuilder Default()
        {
            return new CompanyBuilder()
                .WithId(GetRandom.UniqueId())
                .WithIsName(GetRandom.Name());
        }

        public CompanyBuilder WithId(string value)
        {
            id = value;
            return this;
        }

        public CompanyBuilder WithIsName(string value)
        {
            name = value;
            return this;
        }


    }
}
