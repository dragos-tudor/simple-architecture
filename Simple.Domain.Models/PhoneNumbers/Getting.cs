
using System.Data;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static PhoneNumberValidationException GetInvalidCountryCodeError (short countryCode) => new ($"Invalid phone number country code {countryCode}. Should be between 0-{PhoneNumberContraints.MaxCountryCode}");

  static PhoneNumberValidationException GetInvalidExtensionError (short extension) => new ($"Invalid phone number extension {extension}. Should be between 0-{PhoneNumberContraints.MaxExtension}");

  public static PhoneNumberValidationException GetInvalidPhoneNumberError (long number) => new ($"Invalid phone number {number}. Should be between 0-{PhoneNumberContraints.MaxNumber}");

  public static PhoneNumberDuplicateException GetDuplicatePhoneNumberError (PhoneNumber phoneNumber) => new ($"Duplicate phone number with country code {phoneNumber.CountryCode} and number {phoneNumber.Number}.");

  public static IEnumerable<PhoneNumberDuplicateException> GetDuplicatePhoneNumberErrors (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers.Select(GetDuplicatePhoneNumberError);
}