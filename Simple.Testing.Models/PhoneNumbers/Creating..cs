
namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static PhoneNumber CreateTestPhoneNumber(
    short? countryCode = default,
    long? number = default,
    short? extension = default,
    PhoneNumberType? phoneNumberType = PhoneNumberType.Mobile)
  =>
    new()
    {
      CountryCode = countryCode ?? GetRandomShort(0, PhoneNumberContraints.MaxCountryCode),
      Number = number ?? GetRandomLong(0, PhoneNumberContraints.MaxNumber),
      NumberType = phoneNumberType ?? GetRandomEnum<PhoneNumberType>(),
      Extension = extension ?? GetRandomShort(0, PhoneNumberContraints.MaxExtension)
    };
}
