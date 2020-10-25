using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;

namespace ScooterRental.Core.Services
{
    public class RentalCompany : IRentalCompany
    {
        public Company Company { get; private set; }

        readonly IStartRentHandler startRentHandler;
        readonly IEndRentHandler endRentHandler;
        readonly ICalculateIncomeHandler calculateIncomeHandler;

        public RentalCompany(IStartRentHandler startRentHandler, Company company, IEndRentHandler endRentHandler, ICalculateIncomeHandler calculateIncomeHandler)
        {
            this.startRentHandler = startRentHandler;
            Company = company;
            this.endRentHandler = endRentHandler;
            this.calculateIncomeHandler = calculateIncomeHandler;
        }

        public string Name => Company.Name;

        public void StartRent(string id)
        {
            startRentHandler.Handle(id, Company);
        }

        public decimal EndRent(string id)
        {
            decimal totalCost = endRentHandler.Handle(id, Company.Id);
            // TODO: Call event that rent ended so company price can be calcualted.
        }
        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return calculateIncomeHandler.Handle(year, includeNotCompletedRentals, Company.Id);
        }

    }
}
