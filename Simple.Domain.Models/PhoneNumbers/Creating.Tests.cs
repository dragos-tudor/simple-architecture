
namespace Simple.Domain.Models;

partial class ModelsTests
{
  public static PhoneNumber CreateTestPhoneNumber (
    short? countryCode = default,
    long? number = default,
    string? extension = default,
    PhoneNumberType? phoneNumberType = PhoneNumberType.Mobile)
  =>
    new () {
      CountryCode = countryCode ?? GetRandomShort(0, PhoneNumberContraints.MaxCountryCode),
      Number = number ?? GetRandomLong(0, PhoneNumberContraints.MaxNumber),
      NumberType = phoneNumberType ?? GetRandomEnum<PhoneNumberType>(),
      Extension = extension ?? GetRandomString(PhoneNumberContraints.ExtensionMaxLength)
    };

}