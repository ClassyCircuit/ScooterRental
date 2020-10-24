namespace ScooterRental.Core.Interfaces.Validators
{
    public interface IAddScooterValidator
    {
        void Validate(decimal pricePerMinute);
        void Validate(string id);
    }
}