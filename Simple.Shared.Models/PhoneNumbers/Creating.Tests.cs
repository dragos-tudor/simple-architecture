
using System.Globalization;

namespace Simple.Shared.Models;

partial class ModelsTests
{
  public static PhoneNumber CreateTestPhoneNumber (
    string? countryCode = default,
    string? number = default,
    string? extension = default,
    PhoneNumberType? phoneNumberType = PhoneNumberType.Mobile)
  =>
    new () {
      CountryCode = countryCode ?? GetRandomString(PhoneNumberContraints.CountryCodeMaxLength),
      Number = number ?? GetRandomInt(0, PhoneNumberContraints.NumberLength).ToString(CultureInfo.InvariantCulture),
      NumberType = phoneNumberType ?? GetRandomEnum<PhoneNumberType>(),
      Extension = extension ?? GetRandomString(PhoneNumberContraints.ExtensionMaxLength)
    };

}
