using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Interfaces.Usecases;
using ScooterRental.Core.Interfaces.Validators;
using ScooterRental.Core.Usecases.AddScooter;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class AddScootersTests : TestBase
    {
        private Mock<IAddScooterValidator> AddScooterValidator;

        public AddScootersTests(Context context) : base(context)
        {
            AddScooterValidator = new Mock<IAddScooterValidator>();
        }

        [Fact]
        public void AddScooterValidator_NotUniqueId_ThrowsException()
        {
            var getScooterByIdHandler = new Mock<IGetScooterByIdHandler>();
            getScooterByIdHandler.Setup(x=>x.Handle(Context.ExistingScooterId)).Returns(Context.Scooters[0]);

            AddScooterValidator validator = new AddScooterValidator(getScooterByIdHandler.Object);

            Action act = () => validator.Validate(Context.ExistingScooterId);

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
            Context.ScooterService.Setup(x => x.GetScooterById("1")).Returns((Scooter)null);

            AddScooterHandler handler = new AddScooterHandler(Context.ScooterService.Object, AddScooterValidator.Object);

            handler.Handle("1", 4m);
        }
    }
}
