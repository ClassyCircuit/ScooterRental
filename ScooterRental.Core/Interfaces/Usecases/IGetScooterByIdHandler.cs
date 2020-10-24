using ScooterRental.Core.Entities;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IGetScooterByIdHandler
    {
        Scooter Handle(string id);
    }
}