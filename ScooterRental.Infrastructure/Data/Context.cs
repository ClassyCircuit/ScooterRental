using ScooterRental.Core.Entities;
using System.Collections.Generic;

namespace ScooterRental.Infrastructure.Data
{
    /// <summary>
    /// Holds application entities. 
    /// Company is the aggregate root in this case.
    /// </summary>
    public class Context
    {
        public Context(IList<Company> company)
        {
            Company = company;
        }

        public IList<Company> Company { get; set; }

    }
}
