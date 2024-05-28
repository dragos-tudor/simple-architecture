
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<PhoneNumber> FindPhoneNumber (IQueryable<PhoneNumber> query, PhoneNumber phoneNumber) =>
    FindPhoneNumberByKey(query, phoneNumber.CountryCode, phoneNumber.Number);

  public static IQueryable<PhoneNumber> FindPhoneNumberByKey (IQueryable<PhoneNumber> query, short countryCode, long number) =>
    query.Where(phoneNumber => phoneNumber.CountryCode == countryCode && phoneNumber.Number == number );

  public static IQueryable<PhoneNumber> FindPhoneNumbersWithNumbers (IQueryable<PhoneNumber> query, IEnumerable<long> numbers) =>
    query.Where(phoneNumber => numbers.Contains(phoneNumber.Number));

  public static async Task<IEnumerable<PhoneNumber>> FindPhoneNumbersWithPhoneNumbers (IQueryable<PhoneNumber> query, IEnumerable<PhoneNumber> phoneNumbers, CancellationToken cancellationToken = default) =>
    (await FindPhoneNumbersWithNumbers(query, phoneNumbers.Select(e => e.Number)).ToListAsync(cancellationToken))
      .Where(outer => phoneNumbers.Any(inner => outer == inner));
}