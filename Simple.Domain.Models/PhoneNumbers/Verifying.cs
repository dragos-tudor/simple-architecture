
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static bool IsValidCountryCode (short countryCode) => countryCode <= PhoneNumberContraints.MaxCountryCode;

  static bool IsValidExtension (short? extension) => extension is null? true: extension <= PhoneNumberContraints.MaxExtension;

  static bool IsValidPhoneNumber (long phoneNumber) => phoneNumber <= PhoneNumberContraints.MaxNumber;

  public static bool ExistPhoneNumber (PhoneNumber? phoneNumber) => phoneNumber is not null;

  public static bool ExistsPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers?.Any() ?? false;
}