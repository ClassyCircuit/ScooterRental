using Moq;
using ScooterRental.Core.Entities;
using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Usecases.AddScooter;
using ScooterRental.UnitTests.Setup;
using Shouldly;
using System;
using Xunit;

namespace ScooterRental.UnitTests.Usecases
{
    public class AddScootersTests : TestBase
    {
        private Mock<AddScooterValidator> AddScooterValidator;

        public AddScootersTests(Context context) : base(context)
        {
            AddScooterValidator = new Mock<AddScooterValidator>(Context.ScooterService.Object);
        }

        [Fact]
        public void AddScooterValidator_NotUniqueId_ThrowsException()
        {
            AddScooterValidator validator = new AddScooterValidator(Context.ScooterService.Object);

            Action act = () => validator.Validate(Context.ExistingScooterId);

            Should.Throw<IdNotUniqueException>(act);
        }

        [Fact]
        public void AddScooterValidator_InvalidId_ThrowsException()
        {
            AddScooterValidator validator = new AddScooterValidator(Context.ScooterService.Object);

            Action act = () => validator.Validate("");

            Should.Throw<IdCannotBeEmptyException>(act);
        }

        [Fact]
        public void AddScooterValidator_NegativePrice_ThrowsException()
        {
            AddScooterValidator validator = new AddScooterValidator(Context.ScooterService.Object);

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
