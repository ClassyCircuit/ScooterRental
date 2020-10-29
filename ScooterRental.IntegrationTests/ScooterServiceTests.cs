using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Services.Builders;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.IntegrationTests
{
    [CollectionDefinition("Non-Parallel Collection2", DisableParallelization = true)]
    public class ScooterServiceTests
    {
        private readonly IGetScooterServiceHandler getScooterServiceHandler;
        private readonly IGetRentalCompanyHandler getRentalCompanyHandler;
        private readonly IBusinessLogicRepository businessLogicRepository;
        private readonly ICompanyRepository companyRepository;
        private IScooterService scooterService;
        private IRentalCompany rentalCompany;
        private Company company;

        public ScooterServiceTests(IGetScooterServiceHandler getScooterServiceHandler, IGetRentalCompanyHandler getRentalCompanyHandler, IBusinessLogicRepository businessLogicRepository, ICompanyRepository companyRepository)
        {
            this.getScooterServiceHandler = getScooterServiceHandler;
            this.getRentalCompanyHandler = getRentalCompanyHandler;
            this.businessLogicRepository = businessLogicRepository;
            this.companyRepository = companyRepository;

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
            //rentalCompany = getRentalCompanyHandler.Handle(company.Name);
        }

        [Fact]
        public void CanAddScooter()
        {
            string scooterId = "sc1";
            scooterService.AddScooter(scooterId, 5m);

            var scooter = scooterService.GetScooterById(scooterId);

            scooter.Id.ShouldBe(scooterId);
            scooter.PricePerMinute.ShouldBe(5m);
        }

        [Fact]
        public void CanRemoveScooter()
        {
            string scooterId = "sc1";
            scooterService.AddScooter(scooterId, 5m);
            scooterService.RemoveScooter(scooterId);

            Action act = () => scooterService.GetScooterById(scooterId);

            Should.Throw<EntityDoesNotExistException>(act);
        }

        [Fact]
        public void CanGetAllScooters()
        {
            string scooterId = "sc1";
            string scooterId2 = "sc2";
            scooterService.AddScooter(scooterId, 5m);
            scooterService.AddScooter(scooterId2, 7m);

            var scooters = scooterService.GetScooters();

            scooters.Count.ShouldBe(2);
        }
    }
}
