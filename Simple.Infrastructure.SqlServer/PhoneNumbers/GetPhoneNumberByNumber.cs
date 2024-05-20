
namespace Simple.Domain.Services;

partial class DomainFuncs
{
  static IQueryable<PhoneNumber> GetContactPhoneNumberSpec (IQueryable<PhoneNumber> query, string number, string? countryCode = default) =>
    query.Where(phoneNumber => phoneNumber.Number == number && phoneNumber.CountryCode == countryCode);
}