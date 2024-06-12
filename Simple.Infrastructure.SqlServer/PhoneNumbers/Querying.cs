
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  // https://github.com/dotnet/EntityFramework.Docs/issues/2607
  public static async Task<IEnumerable<PhoneNumber>> FindPhoneNumbers (IQueryable<PhoneNumber> query, IEnumerable<PhoneNumber> phoneNumbers, CancellationToken cancellationToken = default) =>
    (await ModelsFuncs.FindPhoneNumbers(query, phoneNumbers.Select(e => e.Number)).ToListAsync(cancellationToken)) // workaround: filter locally few phone numbers
      .Where(outer => phoneNumbers.Any(inner => outer == inner));
}