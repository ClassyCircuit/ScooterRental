using System;
using System.Runtime.Serialization;

namespace ScooterRental.Core.Exceptions
{
    [Serializable]
    internal class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException()
        {
        }

        public EntityDoesNotExistException(string message) : base(message)
        {
        }

        public EntityDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}