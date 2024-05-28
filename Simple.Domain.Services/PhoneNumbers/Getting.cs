
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static string GetInvalidCountryCodeError (short countryCode) => $"Invalid phone number country code {countryCode}. Should be between 0-{PhoneNumberContraints.MaxCountryCode}";

  public static string GetInvalidPhoneNumberError (long number) => $"Invalid phone number {number}. Should be between 0-{PhoneNumberContraints.MaxNumber}";

  public static string GetDuplicatePhoneNumberError (PhoneNumber phoneNumber) => $"Duplicate phone number with country code {phoneNumber.CountryCode} and number {phoneNumber.Number}.";

  public static IEnumerable<string> GetDuplicatePhoneNumberErrors (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers.Select(GetDuplicatePhoneNumberError);
}