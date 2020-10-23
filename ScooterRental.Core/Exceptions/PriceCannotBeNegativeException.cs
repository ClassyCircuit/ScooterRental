using System;

namespace ScooterRental.Core.Exceptions
{
    public class PriceCannotBeNegativeException : Exception
    {
        public PriceCannotBeNegativeException()
        {
        }

        public PriceCannotBeNegativeException(string message) : base(message)
        {
        }

        public PriceCannotBeNegativeException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
