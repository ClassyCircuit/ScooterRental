namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IRemoveScooterHandler
    {
        void Handle(string id, string companyId);
    }
}