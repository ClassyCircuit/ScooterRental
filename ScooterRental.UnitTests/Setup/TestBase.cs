namespace ScooterRental.UnitTests.Setup
{
    public abstract class TestBase
    {
        protected readonly Mocks Mocks;

        protected TestBase(Mocks mocks)
        {
            Mocks = mocks;
        }
    }
}
