
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<PhoneNumber> FindPhoneNumberByCountryCodeAndNumber (
    IQueryable<PhoneNumber> query,
    string countryCode,
    long number)
  =>
    query.Where(phoneNumber =>
      phoneNumber.CountryCode == countryCode &&
      phoneNumber.Number == number
    );
}