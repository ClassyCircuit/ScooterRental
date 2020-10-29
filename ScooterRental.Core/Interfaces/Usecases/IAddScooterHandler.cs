namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for adding new scooter to the company.
    /// </summary>
    public interface IAddScooterHandler
    {
        void Handle(string id, decimal pricePerMinute, string companyId);
    }
}