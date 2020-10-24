using ScooterRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Infrastructure.Services
{
    public class RentalCompany : IRentalCompany
    {
        // place all expenses on scooter level, not person level
        public string Name => throw new NotImplementedException();

        public void StartRent(string id)
        {
            throw new NotImplementedException();
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
