
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistsPhoneNumber(PhoneNumber? phoneNumber) => phoneNumber is not null;

  public static bool ExistsPhoneNumber(IEnumerable<PhoneNumber> phoneNumbers, PhoneNumber phoneNumber) => phoneNumbers.Any(_phoneNumber => phoneNumber.CountryCode == _phoneNumber.CountryCode && phoneNumber.Number == _phoneNumber.Number);

  static bool IsValidCountryCode(short countryCode) => countryCode <= PhoneNumberContraints.MaxCountryCode;

  static bool IsValidExtension(short? extension) => extension is null || extension <= PhoneNumberContraints.MaxExtension;

  static bool IsValidPhoneNumber(long phoneNumber) => phoneNumber <= PhoneNumberContraints.MaxNumber;
}