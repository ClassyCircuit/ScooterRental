using System;
using System.Runtime.Serialization;

namespace ScooterRental.Core.Exceptions
{
    public class ScooterIsNotRentedException : Exception
    {
        public ScooterIsNotRentedException()
        {
        }

        public ScooterIsNotRentedException(string message) : base(message)
        {
        }

        public ScooterIsNotRentedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ScooterIsNotRentedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}