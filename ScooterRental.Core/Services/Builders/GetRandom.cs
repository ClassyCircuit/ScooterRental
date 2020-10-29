using System;
using System.Collections.Generic;

namespace ScooterRental.Core.Services.Builders
{
    /// <summary>
    /// Random data generator for builders.
    /// </summary>
    public static class GetRandom
    {
        private static List<string> names = new List<string>()
        {
            "Clean Air Rental",
            "Mann & Overton",
            "SMRT Rental",
            "Yellow Cab Rental",
            "Checker Taxi",
            "Rental International",
            "Yellow Tomato Rental",
        };

        public static string Name()
        {
            return names[Random.Next(0, names.Count - 1)];
        }

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
