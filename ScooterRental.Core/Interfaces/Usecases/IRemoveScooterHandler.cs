namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for removing scooter from a company.
    /// </summary>
    public interface IRemoveScooterHandler
    {
        void Handle(string id, string companyId);
    }
}