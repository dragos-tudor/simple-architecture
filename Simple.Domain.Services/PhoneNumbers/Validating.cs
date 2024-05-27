
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static string? ValidateCountryCode (short countryCode) => !IsValidCountryCode(countryCode)? GetInvalidCountryCodeError(countryCode): default;

  static string? ValidatePhoneNumber (long phoneNumber) => !IsValidPhoneNumber(phoneNumber)? GetInvalidPhoneNumberError(phoneNumber): default;

  public static IEnumerable<string?> ValidatePhoneNumber (PhoneNumber phoneNumber) => GetValidationErrors([
    ValidateCountryCode(phoneNumber.CountryCode),
    ValidatePhoneNumber(phoneNumber.Number)
  ]);

  public static IEnumerable<string?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.SelectMany(ValidatePhoneNumber).Where(ExistValidationError);
}