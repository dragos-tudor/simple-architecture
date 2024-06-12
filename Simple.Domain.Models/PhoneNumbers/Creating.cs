
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static PhoneNumber CreatePhoneNumber(short countryCode, long number) => new () {CountryCode = countryCode, Number = number };
}