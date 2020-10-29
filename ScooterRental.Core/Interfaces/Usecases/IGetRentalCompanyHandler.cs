using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for retrieving IRentalCompany component.
    /// </summary>
    public interface IGetRentalCompanyHandler
    {
        /// <summary>
        /// Get rental company by name.
        /// </summary>
        /// <param name="name">name of company</param>
        /// <returns></returns>
        IRentalCompany Handle(string name);
    }
}