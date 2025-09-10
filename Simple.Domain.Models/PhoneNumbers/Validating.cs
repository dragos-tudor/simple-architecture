
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static string? ValidateCountryCode(short countryCode) => !IsValidCountryCode(countryCode) ? InvalidCountryCodeError : default;

  static string? ValidateExtension(short? extension) => !IsValidExtension(extension) ? InvalidExtensionError : default;

  static string? ValidatePhoneNumber(long phoneNumber) => !IsValidPhoneNumber(phoneNumber) ? InvalidPhoneNumberError : default;

  public static string?[] ValidatePhoneNumber(PhoneNumber phoneNumber) => [
    ValidateCountryCode(phoneNumber.CountryCode),
    ValidatePhoneNumber(phoneNumber.Number),
    ValidateExtension(phoneNumber.Extension)
  ];
}