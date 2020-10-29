namespace ScooterRental.Core.Interfaces.Validators
{
    /// <summary>
    /// Validator for RemoveScooter usecase.
    /// </summary>
    public interface IRemoveScooterValidator
    {
        void Validate(string id, string companyId);
    }
}