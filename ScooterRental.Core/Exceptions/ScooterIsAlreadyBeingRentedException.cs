using System;
using System.Runtime.Serialization;

namespace ScooterRental.Core.Exceptions
{
    [Serializable]
    public class ScooterIsAlreadyBeingRentedException : Exception
    {
        public ScooterIsAlreadyBeingRentedException()
        {
        }

        public ScooterIsAlreadyBeingRentedException(string message) : base(message)
        {
        }

        public ScooterIsAlreadyBeingRentedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ScooterIsAlreadyBeingRentedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}