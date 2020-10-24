using ScooterRental.Core.Entities;

namespace ScooterRental.UnitTests.Builders
{
    public class ScooterBuilder
    {
        private Scooter scooter;

        private string id;
        private decimal pricePerMinute;
        private bool isRented;

        public Scooter Build()
        {
            if(scooter == null)
            {
                scooter = new Scooter(id, pricePerMinute);
                scooter.IsRented = isRented;
            }

            return scooter;
        }

        public static ScooterBuilder Default()
        {
            return new ScooterBuilder()
                .WithId(GetRandom.UniqueId())
                .WithPricePerMinute(GetRandom.Decimal(0, 5))
                .WithIsRented(false);
        }

        public ScooterBuilder WithId(string value)
        {
            id = value;
            return this;
        }

        public ScooterBuilder WithPricePerMinute(decimal value)
        {
            pricePerMinute = value;
            return this;
        }

        public ScooterBuilder WithIsRented(bool value)
        {
            isRented = value;
            return this;
        }
    }
}
