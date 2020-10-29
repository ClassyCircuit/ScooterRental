using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IGetRentalCompanyHandler
    {
        IRentalCompany Handle(string name);
    }
}