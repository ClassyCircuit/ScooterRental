using System;

namespace ScooterRental.UnitTests.Builders
{
    /// <summary>
    /// Random data generator
    /// </summary>
    public static class GetRandom
    {
        public static Random Random { get; set; } = new Random();

        public static string UniqueId()
        {
            return Guid.NewGuid().ToString();
        }

        public static decimal Decimal(int minValue, int maxValue)
        {
            decimal value = minValue + (maxValue - minValue) * Convert.ToDecimal(Double());
            return value;
        }

        public static double Double()
        {
            return Random.NextDouble();
        }
    }
}
