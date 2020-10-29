namespace ScooterRental.Core.Interfaces.Validators
{
    /// <summary>
    /// Validator for GetScooterById usecase.
    /// </summary>
    public interface IGetScooterByIdValidator
    {
        void Validate(string id);
    }
}