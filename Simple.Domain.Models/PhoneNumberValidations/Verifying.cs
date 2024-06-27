
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static bool IsValidCountryCode (short countryCode) => countryCode <= PhoneNumberContraints.MaxCountryCode;

  static bool IsValidExtension (short? extension) => extension is null? true: extension <= PhoneNumberContraints.MaxExtension;

  static bool IsValidPhoneNumber (long phoneNumber) => phoneNumber <= PhoneNumberContraints.MaxNumber;
}