using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Services;

namespace ScooterRental.Core.Usecases
{
    /// <summary>
    /// Usecase returns ScooterService for a given company.
    /// </summary>
    public class GetScooterServiceHandler : IGetScooterServiceHandler
    {
        private readonly ICompanyRepository companyRepository;
        private readonly AddScooterHandler addScooterHandler;
        private readonly GetScooterByIdHandler getScooterByIdHandler;
        private readonly GetScootersHandler getScootersHandler;
        private readonly RemoveScooterHandler removeScooterHandler;

        public IScooterService Handle(string name)
        {
            var company = companyRepository.GetCompanyByName(name);

            if (company == null)
            {
                throw new EntityDoesNotExistException($"Company with name: {name} does not exist.");
            }

            return new ScooterService(addScooterHandler, getScooterByIdHandler, getScootersHandler, removeScooterHandler, company);
        }
    }
}
