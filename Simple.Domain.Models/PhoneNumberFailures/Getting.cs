
using System.Data;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static PhoneNumberValidationFailure GetInvalidCountryCodeFailure (short countryCode) => new ($"Invalid phone number country code {countryCode}. Should be between 0-{PhoneNumberContraints.MaxCountryCode}");

  static PhoneNumberValidationFailure GetInvalidExtensionFailure (short extension) => new ($"Invalid phone number extension {extension}. Should be between 0-{PhoneNumberContraints.MaxExtension}");

  public static PhoneNumberValidationFailure GetInvalidPhoneNumberFailure (long number) => new ($"Invalid phone number {number}. Should be between 0-{PhoneNumberContraints.MaxNumber}");

  public static PhoneNumberDuplicateFailure GetDuplicatePhoneNumberFailure (PhoneNumber phoneNumber) => new ($"Duplicate phone number with country code {phoneNumber.CountryCode} and number {phoneNumber.Number}.");

  public static IEnumerable<PhoneNumberDuplicateFailure> GetDuplicatePhoneNumberFailures (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers.Select(GetDuplicatePhoneNumberFailure);
}