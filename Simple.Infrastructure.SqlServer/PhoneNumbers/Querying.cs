
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<PhoneNumber> FindPhoneNumber (IQueryable<PhoneNumber> query, PhoneNumber phoneNumber) =>
    query.Where(_phoneNumber => _phoneNumber.CountryCode == phoneNumber.CountryCode && _phoneNumber.Number == phoneNumber.Number );

  // https://github.com/dotnet/EntityFramework.Docs/issues/2607
  public static async Task<IEnumerable<PhoneNumber>> FindPhoneNumbers (IQueryable<PhoneNumber> query, IEnumerable<PhoneNumber> phoneNumbers, CancellationToken cancellationToken = default) =>
    (await FindPhoneNumbersWithNumbers(query, phoneNumbers.Select(e => e.Number)).ToListAsync(cancellationToken)) // workaround: filter locally few phone numbers
      .Where(outer => phoneNumbers.Any(inner => outer == inner));

  public static IQueryable<PhoneNumber> FindPhoneNumbersWithNumbers (IQueryable<PhoneNumber> query, IEnumerable<long> numbers) =>
    query.Where(phoneNumber => numbers.Contains(phoneNumber.Number));
}