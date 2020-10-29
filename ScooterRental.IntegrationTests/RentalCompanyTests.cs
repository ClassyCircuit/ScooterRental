using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Services.Builders;
using Shouldly;
using System.Threading;
using Xunit;

namespace ScooterRental.IntegrationTests
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class RentalCompanyTests
    {
        private readonly IGetScooterServiceHandler getScooterServiceHandler;
        private readonly IGetRentalCompanyHandler getRentalCompanyHandler;
        private readonly IBusinessLogicRepository businessLogicRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IRentEventRepository rentEventRepository;
        private IScooterService scooterService;
        private IRentalCompany rentalCompany;
        private Company company;
        private string scooterId = "someId";

        public RentalCompanyTests(IGetScooterServiceHandler getScooterServiceHandler, IGetRentalCompanyHandler getRentalCompanyHandler, IBusinessLogicRepository businessLogicRepository, ICompanyRepository companyRepository, IRentEventRepository rentEventRepository)
        {
            this.getScooterServiceHandler = getScooterServiceHandler;
            this.getRentalCompanyHandler = getRentalCompanyHandler;
            this.businessLogicRepository = businessLogicRepository;
            this.companyRepository = companyRepository;
            this.rentEventRepository = rentEventRepository;

            Initialize();
        }

        /// <summary>
        /// Inserts initial data needed for application logic to function.
        /// </summary>
        private void Initialize()
        {
            company = CompanyBuilder.Default().Build();
            PriceLimit priceLimit = PriceLimitBuilder.Default(company).Build();

            companyRepository.AddCompany(company);
            businessLogicRepository.UpdatePriceLimit(priceLimit, company.Id);

            scooterService = getScooterServiceHandler.Handle(company.Name);
            rentalCompany = getRentalCompanyHandler.Handle(company.Name);

            scooterService.AddScooter(scooterId, 1.5m);
            var rentEvent = RentEventBuilder.Default(company, ScooterBuilder.Default(company).Build()).WithIsActive(false).WithTotalPrice(10).Build();

            rentEventRepository.CreateRentEvent(company.Id, rentEvent);
        }

        [Fact]
        public void CanGetCompanyName()
        {
            rentalCompany.Name.ShouldBe(company.Name);
        }

        [Fact]
        public void CanStartAndEndRent()
        {
            rentalCompany.StartRent(scooterId);

            var rentEvent = rentEventRepository.GetActiveRentEventByScooterId(company.Id, scooterId);
            rentEvent.IsActive.ShouldBeTrue();

            Thread.Sleep(2000);

            rentalCompany.EndRent(scooterId);
            var completedEvents = rentEventRepository.GetCompletedRentalsByYear(company.Id, null);

            completedEvents.Count.ShouldBe(1);
            completedEvents[0].IsActive.ShouldBeFalse();
        }

        [Fact]
        public void CanCalculateIncomeForCompletedEvents()
        {
            var total = rentalCompany.CalculateIncome(null, false);

            total.ShouldBe(10m);
        }
    }
}
