namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IAddScooterHandler
    {
        void Handle(string id, decimal pricePerMinute);
    }
}