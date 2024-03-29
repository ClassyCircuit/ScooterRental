﻿using Moq;
using ScooterRental.Core.Entities;
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
    public class AddScootersTests : TestBase
    {
        private Mock<IAddScooterValidator> AddScooterValidator;
        private Mock<IScooterRepository> ScooterRepository;

        public AddScootersTests(Setup.Data context) : base(context)
        {
            AddScooterValidator = new Mock<IAddScooterValidator>();
            ScooterRepository = new Mock<IScooterRepository>();
        }

        [Fact]
        public void AddScooterValidator_NotUniqueId_ThrowsException()
        {
            var getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            getScooterByIdHandler.Setup(x => x.Handle(Data.ExistingScooterId, Data.Company.Id)).Returns(Data.Scooters[0]);

            AddScooterValidator validator = new AddScooterValidator(getScooterByIdHandler.Object);

            Action act = () => validator.Validate(Data.ExistingScooterId, Data.Company.Id);

            Should.Throw<IdNotUniqueException>(act);
        }

        [Fact]
        public void AddScooterValidator_NegativePrice_ThrowsException()
        {
            AddScooterValidator validator = new AddScooterValidator(new Mock<IGetScooterByIdHandler>().Object);

            Action act = () => validator.Validate(-5m);

            Should.Throw<PriceCannotBeNegativeException>(act);
        }

        [Fact]
        public void AddScooter_AddsNewScooter()
        {
            ScooterRepository.Setup(x => x.GetScooterById(Data.Company.Id, "1")).Returns((Scooter)null);

            AddScooterHandler handler = new AddScooterHandler(ScooterRepository.Object, AddScooterValidator.Object);

            handler.Handle("1", 4m, Data.Company.Id);
        }
    }
}
