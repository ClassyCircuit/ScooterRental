using ScooterRental.Core.Interfaces.Services;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IGetScooterServiceHandler
    {
        IScooterService Handle(string name);
    }
}