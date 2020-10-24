using ScooterRental.Core.Exceptions;
using System;

namespace ScooterRental.Core.Usecases.GetScooterById
{
    public class GetScooterByIdValidator
    {
        public void Validate(string id)
        {
            if (id == "")
            {
                throw new IdCannotBeEmptyException("Scooter ID must have a value");
            }
        }
    }
}