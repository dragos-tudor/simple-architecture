
namespace Simple.Shared.Models;

partial class ModelsTests
{
  public static PhoneNumber CreateTestPhoneNumber (
    string? countryCode = default,
    long? number = default,
    string? extension = default,
    PhoneNumberType? phoneNumberType = PhoneNumberType.Mobile)
  =>
    new () {
      CountryCode = countryCode ?? GetRandomString(PhoneNumberContraints.CountryCodeMaxLength),
      Number = number ?? GetRandomLong(0, PhoneNumberContraints.MaxNumber),
      NumberType = phoneNumberType ?? GetRandomEnum<PhoneNumberType>(),
      Extension = extension ?? GetRandomString(PhoneNumberContraints.ExtensionMaxLength)
    };

}
