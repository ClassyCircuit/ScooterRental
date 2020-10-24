using System;
using System.Runtime.Serialization;

namespace ScooterRental.Core.Exceptions
{
    [Serializable]
    public class RentedScooterCannotBeRemovedException : Exception
    {
        public RentedScooterCannotBeRemovedException()
        {
        }

        public RentedScooterCannotBeRemovedException(string message) : base(message)
        {
        }

        public RentedScooterCannotBeRemovedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RentedScooterCannotBeRemovedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}