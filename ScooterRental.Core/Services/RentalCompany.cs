using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using System;

namespace ScooterRental.Core.Services
{
    public class RentalCompany : IRentalCompany
    {
        readonly IStartRentHandler startRentHandler;

        public RentalCompany(IStartRentHandler startRentHandler)
        {
            this.startRentHandler = startRentHandler;
        }

        public string Name => throw new NotImplementedException();

        public void StartRent(string id)
        {
            startRentHandler.Handle(id);
        }

        public decimal EndRent(string id)
        {
            throw new NotImplementedException();
        }
        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }

    }
}
