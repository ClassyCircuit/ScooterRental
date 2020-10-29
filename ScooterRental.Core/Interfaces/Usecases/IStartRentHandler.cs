using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for starting a new rent on a scooter.
    /// </summary>
    public interface IStartRentHandler
    {
        void Handle(string id, Company companyId);
    }
}