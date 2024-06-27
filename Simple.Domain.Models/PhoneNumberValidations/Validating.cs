
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static PhoneNumberValidationFailure? ValidateCountryCode (short countryCode) => !IsValidCountryCode(countryCode)? GetInvalidCountryCodeFailure(countryCode): default;

  static PhoneNumberValidationFailure? ValidateExtension (short? extension) => !IsValidExtension(extension)? GetInvalidExtensionFailure(extension!.Value): default;

  static PhoneNumberValidationFailure? ValidatePhoneNumber (long phoneNumber) => !IsValidPhoneNumber(phoneNumber)? GetInvalidPhoneNumberFailure(phoneNumber): default;

  public static IEnumerable<PhoneNumberValidationFailure?> ValidatePhoneNumber (PhoneNumber phoneNumber) => [
    ValidateCountryCode(phoneNumber.CountryCode),
    ValidatePhoneNumber(phoneNumber.Number),
    ValidateExtension(phoneNumber.Extension)
  ];

  public static IEnumerable<PhoneNumberValidationFailure?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.SelectMany(ValidatePhoneNumber);
}