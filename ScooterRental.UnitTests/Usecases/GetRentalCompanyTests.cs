using Moq;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Usecases;
using ScooterRental.Core.Validators;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class GetRentalCompanyTests : TestBase
    {
        Mock<IStartRentHandler> IStartRentHandler;
        Mock<IEndRentHandler> IEndRentHandler;
        Mock<ICalculateIncomeHandler> ICalculateIncomeHandler;
        Mock<IGetRentalCompanyValidator> IGetRentalCompanyValidator;

        GetRentalCompanyHandler GetRentalCompanyHandler;


        public GetRentalCompanyTests(Setup.Mocks mocks) : base(mocks)
        {
            IStartRentHandler = new Mock<IStartRentHandler>();
            IEndRentHandler = new Mock<IEndRentHandler>();
            ICalculateIncomeHandler = new Mock<ICalculateIncomeHandler>();
            IGetRentalCompanyValidator = new Mock<IGetRentalCompanyValidator>();

            GetRentalCompanyHandler = new GetRentalCompanyHandler(Mocks.CompanyRepository.Object, IStartRentHandler.Object, IEndRentHandler.Object, ICalculateIncomeHandler.Object, IGetRentalCompanyValidator.Object);
        }

        [Fact]
        public void Handle_ValidCompanyName_ReturnsRentalCompany()
        {
            IRentalCompany rentalCompany = GetRentalCompanyHandler.Handle(Mocks.Company.Name);

            rentalCompany.Name.ShouldBe(Mocks.Company.Name);
        }

        [Fact]
        public void Handle_InvalidCompanyName_ThrowsException()
        {
            Action act = () => GetRentalCompanyHandler.Handle("someName");

            Should.Throw<EntityDoesNotExistException>(act);

        }

        [Fact]
        public void Validator_EmptyCompanyName_ThrowsException()
        {
            GetRentalCompanyValidator validator = new GetRentalCompanyValidator();

            Action act = () => validator.Validate("");

            Should.Throw<IdCannotBeEmptyException>(act);
        }
    }
}
