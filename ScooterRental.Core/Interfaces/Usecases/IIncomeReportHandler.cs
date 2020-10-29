using System;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for reporting company's income from renting scooters.
    /// </summary>
    public interface IIncomeReportHandler
    {
        decimal Handle(int? year, bool includeNotCompletedRentals, string companyId, DateTime endDate);
    }
}