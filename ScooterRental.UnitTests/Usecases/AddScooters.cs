using ScooterRental.Core.Exceptions;
using ScooterRental.Core.Usecases.AddScooter;
using System;
using Xunit;

namespace ScooterRental.UnitTests
{
    public class AddScooters
    {
        private readonly Context context;

        public AddScooters(Context context)
        {
            this.context = context;
        }

        [Fact]
        public void AddScooterValidator_NotUniqueId_ThrowsException()
        {
            AddScooterValidator validator = new AddScooterValidator(context.ScooterService.Object);

            Action act = () => validator.Validate(context.ExistingScooterId);

            Assert.Throws<IdNotUniqueException>(act);
        }

        [Fact]
        public void AddScooterValidator_NegativePrice_ThrowsException()
        {
            AddScooterValidator validator = new AddScooterValidator(context.ScooterService.Object);

            Action act = () => validator.Validate(-5m);

            Assert.Throws<PriceCannotBeNegativeException>(act);
        }
    }
}
