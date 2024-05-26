
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static string? ValidateCountryCode (short countryCode) =>
    !IsValidCountryCode(countryCode)? GetInvalidCountryCodeError(countryCode): default;

  static string? ValidatePhoneNumber (long phoneNumber) =>
    !IsValidPhoneNumber(phoneNumber)? GetInvalidPhoneNumberError(phoneNumber): default;

  public static IEnumerable<string?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.SelectMany(ValidatePhoneNumber).Where(ExistValidationError);

  public static IEnumerable<string?> ValidatePhoneNumber (PhoneNumber phoneNumber) =>
    GetValidationErrors([
      ValidateCountryCode(phoneNumber.CountryCode),
      ValidatePhoneNumber(phoneNumber.Number)
    ]);
}