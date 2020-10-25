namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface ICalculateIncomeHandler
    {
        decimal Handle(int? year, bool includeNotCompletedRentals, string companyId);
    }
}