
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<PhoneNumber> GetContactPhoneNumber (IQueryable<PhoneNumber> query, string number, string? countryCode = default) =>
    query.Where(phoneNumber => phoneNumber.Number == number && phoneNumber.CountryCode == countryCode);
}