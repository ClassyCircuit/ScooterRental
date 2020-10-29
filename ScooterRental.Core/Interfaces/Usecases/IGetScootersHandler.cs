using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for retrieving all scooters that belong to a company.
    /// </summary>
    public interface IGetScootersHandler
    {
        IList<Scooter> Handle(string companyId);
    }
}