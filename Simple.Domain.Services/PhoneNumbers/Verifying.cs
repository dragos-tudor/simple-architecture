
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static bool IsValidCountryCode (short countryCode) => countryCode <= PhoneNumberContraints.MaxCountryCode;

  static bool IsValidPhoneNumber (long phoneNumber) => phoneNumber <= PhoneNumberContraints.MaxNumber;

  public static bool ExistPhoneNumber (PhoneNumber? phoneNumber) => phoneNumber is not null;

  public static bool ExistsPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers?.Any() ?? false;
}