using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for retrieving IRentalCompany component.
    /// </summary>
    public interface IGetRentalCompanyHandler
    {
        IRentalCompany Handle(string name);
    }
}