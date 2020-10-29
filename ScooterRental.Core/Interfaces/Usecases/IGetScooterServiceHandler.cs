using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for retrieving IScooterService component.
    /// </summary>
    public interface IGetScooterServiceHandler
    {
        IScooterService Handle(string name);
    }
}