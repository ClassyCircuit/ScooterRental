using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for retrieving IScooterService component.
    /// </summary>
    public interface IGetScooterServiceHandler
    {
        /// <summary>
        /// Get scooter service by company name.
        /// </summary>
        /// <param name="name">company name</param>
        /// <returns></returns>
        IScooterService Handle(string name);
    }
}