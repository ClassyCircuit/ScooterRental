using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Services.Builders
{
    /// <summary>
    /// Builder for creating new rent event entities.
    /// </summary>
    public class PriceLimitBuilder
    {
        private PriceLimit priceLimit;

        private string id;
        private decimal costLimitPerDay;
        private Company company;

        public PriceLimit Build()
        {
            if (priceLimit == null)
            {
                priceLimit = new PriceLimit(id, costLimitPerDay, company);
            }

            return priceLimit;
        }

        public static PriceLimitBuilder Default(Company company)
        {
            return new PriceLimitBuilder()
                .WithId(GetRandom.UniqueId())
                .WithCompany(company)
                .WithCostLimitPerDay(20m);
        }

        private PriceLimitBuilder WithCostLimitPerDay(decimal value)
        {
            costLimitPerDay = value;
            return this;
        }

        public PriceLimitBuilder WithCompany(Company value)
        {
            company = value;
            return this;
        }

        public PriceLimitBuilder WithId(string value)
        {
            id = value;
            return this;
        }
    }
}
