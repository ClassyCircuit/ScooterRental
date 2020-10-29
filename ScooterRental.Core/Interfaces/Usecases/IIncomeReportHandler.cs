using System;

namespace ScooterRental.Core.Interfaces.Usecases
{
    public interface IIncomeReportHandler
    {
        decimal Handle(int? year, bool includeNotCompletedRentals, string companyId, DateTime endDate);
    }
}