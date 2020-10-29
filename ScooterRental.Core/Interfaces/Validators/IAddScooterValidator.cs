namespace ScooterRental.Core.Interfaces.Validators
{
    /// <summary>
    /// Validator add scooter usecase.
    /// </summary>
    public interface IAddScooterValidator
    {
        void Validate(decimal pricePerMinute);
        void Validate(string id, string companyId);
    }
}