
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static PhoneNumber CreatePhoneNumber(short countryCode, long number, short? extension = default, PhoneNumberType numberType = default) =>
    new() { CountryCode = countryCode, Number = number, Extension = extension, NumberType = numberType };
}