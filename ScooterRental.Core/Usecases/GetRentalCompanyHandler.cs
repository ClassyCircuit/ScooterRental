using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Services;

namespace ScooterRental.Core.Usecases
{
    public class GetRentalCompanyHandler : IGetRentalCompanyHandler
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IStartRentHandler startRentHandler;
        private readonly IEndRentHandler endRentHandler;
        private readonly IIncomeReportHandler calculateIncomeHandler;
        private readonly IGetRentalCompanyValidator validator;

        public GetRentalCompanyHandler(ICompanyRepository companyRepository, IStartRentHandler startRentHandler, IEndRentHandler endRentHandler, IIncomeReportHandler calculateIncomeHandler, IGetRentalCompanyValidator validator)
        {
            this.companyRepository = companyRepository;
            this.startRentHandler = startRentHandler;
            this.endRentHandler = endRentHandler;
            this.calculateIncomeHandler = calculateIncomeHandler;
            this.validator = validator;
        }

        public IRentalCompany Handle(string name)
        {
            // Validation
            validator.Validate(name);
            var company = companyRepository.GetCompanyByName(name);

            // Attempt to retrieve the company
            if (company == null)
            {
                throw new EntityDoesNotExistException($"Company with name: {name} does not exist.");
            }

            return new RentalCompany(startRentHandler, company, endRentHandler, calculateIncomeHandler);
        }
    }
}
