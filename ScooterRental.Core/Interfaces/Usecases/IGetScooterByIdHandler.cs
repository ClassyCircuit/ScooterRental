using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for retrieving scooters by their ids.
    /// </summary>
    public interface IGetScooterByIdHandler
    {
        Scooter Handle(string id, string companyId);
    }
}