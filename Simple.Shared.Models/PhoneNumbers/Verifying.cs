
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static bool IsValidCountryCode (short countryCode) => countryCode <= PhoneNumberContraints.MaxCountryCode;

  static bool IsValidPhoneNumber (long phoneNumber) => phoneNumber <= PhoneNumberContraints.MaxNumber;

  public static bool ExistsPhoneNumber (long? phoneNumber) => phoneNumber is not null;

  public static bool ExistsPhoneNumbers (IEnumerable<long> phoneNumbers) => phoneNumbers.Any();
}