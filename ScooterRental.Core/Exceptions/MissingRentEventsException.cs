using System;
using System.Runtime.Serialization;

namespace ScooterRental.Core.Exceptions
{
    [Serializable]
    internal class MissingRentEventsException : Exception
    {
        public MissingRentEventsException()
        {
        }

        public MissingRentEventsException(string message) : base(message)
        {
        }

        public MissingRentEventsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingRentEventsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}