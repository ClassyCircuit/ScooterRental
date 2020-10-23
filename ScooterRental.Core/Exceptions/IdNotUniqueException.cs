using System;

namespace ScooterRental.Core.Exceptions
{
    public class IdNotUniqueException : Exception
    {
        public IdNotUniqueException()
        {
        }

        public IdNotUniqueException(string message) : base(message)
        {
        }

        public IdNotUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
