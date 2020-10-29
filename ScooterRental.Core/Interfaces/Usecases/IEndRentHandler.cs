using System;

namespace ScooterRental.Core.Interfaces.Usecases
{
    /// <summary>
    /// Usecase for stopping an active rental event.
    /// </summary>
    public interface IEndRentHandler
    {
        decimal Handle(string scooterId, string companyId, DateTime endDate);
    }
}