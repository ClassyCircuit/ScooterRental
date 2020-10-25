namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IEndRentHandler
    {
        decimal Handle(string scooterId, string companyId);
    }
}