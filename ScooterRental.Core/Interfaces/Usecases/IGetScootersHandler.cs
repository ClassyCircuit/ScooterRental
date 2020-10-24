using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IGetScootersHandler
    {
        IList<Scooter> Handle();
    }
}