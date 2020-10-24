namespace ScooterRental.UnitTests.Setup
{
    public abstract class TestBase
    {
        protected readonly Context Context;

        protected TestBase(Context context)
        {
            Context = context;
        }
    }
}
