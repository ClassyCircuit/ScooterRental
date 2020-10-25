using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IStartRentHandler
    {
        void Handle(string id, Company companyId);
    }
}