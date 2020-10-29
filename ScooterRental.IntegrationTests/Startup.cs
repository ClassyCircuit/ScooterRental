using Microsoft.Extensions.DependencyInjection;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Interfaces.Services;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Services;
using ScooterRental.Core.Usecases;
using ScooterRental.Core.Validators;
using ScooterRental.Infrastructure.Data;
using ScooterRental.Infrastructure.Services;
using System.Collections.Generic;

namespace ScooterRental.IntegrationTests
{
    /// <summary>
    /// Setup dependency injection container for integration tests.
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddScoped<IRentalCostService, RentalCostService>();

            // Usecase handlers
            services.AddScoped<IAddScooterHandler, AddScooterHandler>();
            services.AddScoped<IEndRentHandler, EndRentHandler>();
            services.AddTransient<IGetRentalCompanyHandler, GetRentalCompanyHandler>();
            services.AddScoped<IGetScooterByIdHandler, GetScooterByIdHandler>();
            services.AddTransient<IGetScooterServiceHandler, GetScooterServiceHandler>();
            services.AddScoped<IGetScootersHandler, GetScootersHandler>();
            services.AddScoped<IIncomeReportHandler, IncomeReportHandler>();
            services.AddScoped<IRemoveScooterHandler, RemoveScooterHandler>();
            services.AddScoped<IRentEventUpdateHandler, RentEventUpdateHandler>();
            services.AddScoped<IStartRentHandler, StartRentHandler>();

            // Infrastructure
            services.AddSingleton<Context, Context>();
            services.AddSingleton<IList<Company>, List<Company>>();

            // Validators
            services.AddScoped<IAddScooterValidator, AddScooterValidator>();
            services.AddScoped<IEndRentValidator, EndRentValidator>();
            services.AddScoped<IGetRentalCompanyValidator, GetRentalCompanyValidator>();
            services.AddScoped<IGetScooterByIdValidator, GetScooterByIdValidator>();
            services.AddScoped<IRemoveScooterValidator, RemoveScooterValidator>();
            services.AddScoped<IStartRentValidator, StartRentValidator>();

            // Repositories
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<IScooterRepository, ScooterRepository>();
            services.AddSingleton<IRentEventRepository, RentEventRepository>();
            services.AddSingleton<IBusinessLogicRepository, BusinessLogicRepository>();

        }
    }
}
