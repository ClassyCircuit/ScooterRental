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
        private readonly IAddScooterHandler addScooterHandler;
        private readonly IGetScooterByIdHandler getScooterByIdHandler;
        private readonly IGetScootersHandler getScootersHandler;
        private readonly IRemoveScooterHandler removeScooterHandler;

        public GetScooterServiceHandler(ICompanyRepository companyRepository, IAddScooterHandler addScooterHandler, IGetScooterByIdHandler getScooterByIdHandler, IGetScootersHandler getScootersHandler, IRemoveScooterHandler removeScooterHandler)
        {
            this.companyRepository = companyRepository;
            this.addScooterHandler = addScooterHandler;
            this.getScooterByIdHandler = getScooterByIdHandler;
            this.getScootersHandler = getScootersHandler;
            this.removeScooterHandler = removeScooterHandler;
        }

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
