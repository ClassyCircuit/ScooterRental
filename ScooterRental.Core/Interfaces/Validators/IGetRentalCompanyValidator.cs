namespace ScooterRental.Core.Interfaces.Validators
{
    /// <summary>
    /// Validator for GetRentalCompany usecase.
    /// </summary>
    public interface IGetRentalCompanyValidator
    {
        void Validate(string name);
    }
}