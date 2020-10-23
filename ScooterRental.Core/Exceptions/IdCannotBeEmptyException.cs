using System;
using System.Runtime.Serialization;

namespace ScooterRental.Core.Exceptions
{
    [Serializable]
    internal class IdCannotBeEmptyException : Exception
    {
        public IdCannotBeEmptyException()
        {
        }

        public IdCannotBeEmptyException(string message) : base(message)
        {
        }

        public IdCannotBeEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IdCannotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}