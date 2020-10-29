namespace ScooterRental.UnitTests.Setup
{
    public abstract class TestBase
    {
        protected readonly Data Data;

        protected TestBase(Data data)
        {
            Data = data;
        }
    }
}
