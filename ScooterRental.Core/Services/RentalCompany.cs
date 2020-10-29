using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System;

namespace ScooterRental.Core.Services
{
    public class RentalCompany : IRentalCompany
    {
        public Company Company { get; private set; }

        readonly IStartRentHandler startRentHandler;
        readonly IEndRentHandler endRentHandler;
        readonly IIncomeReportHandler incomeReportHandler;

        public RentalCompany(IStartRentHandler startRentHandler, Company company, IEndRentHandler endRentHandler, IIncomeReportHandler incomeReportHandler)
        {
            this.startRentHandler = startRentHandler;
            Company = company;
            this.endRentHandler = endRentHandler;
            this.incomeReportHandler = incomeReportHandler;
        }

        public string Name => Company.Name;

        public void StartRent(string id)
        {
            startRentHandler.Handle(id, Company);
        }

        public decimal EndRent(string id)
        {
            return endRentHandler.Handle(id, Company.Id, DateTime.UtcNow);
        }
        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return incomeReportHandler.Handle(year, includeNotCompletedRentals, Company.Id, DateTime.UtcNow);
        }

    }
}
