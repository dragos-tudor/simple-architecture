
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static PhoneNumberValidationException? ValidateCountryCode (short countryCode) => !IsValidCountryCode(countryCode)? GetInvalidCountryCodeError(countryCode): default;

  static PhoneNumberValidationException? ValidateExtension (short? extension) => !IsValidExtension(extension)? GetInvalidExtensionError(extension!.Value): default;

  static PhoneNumberValidationException? ValidatePhoneNumber (long phoneNumber) => !IsValidPhoneNumber(phoneNumber)? GetInvalidPhoneNumberError(phoneNumber): default;

  public static IEnumerable<PhoneNumberValidationException?> ValidatePhoneNumber (PhoneNumber phoneNumber) => [
    ValidateCountryCode(phoneNumber.CountryCode),
    ValidatePhoneNumber(phoneNumber.Number),
    ValidateExtension(phoneNumber.Extension)
  ];

  public static IEnumerable<PhoneNumberValidationException?> ValidatePhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.SelectMany(ValidatePhoneNumber);
}