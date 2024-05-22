
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  internal static IEnumerable<PhoneNumber> OrderPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers) => phoneNumbers.OrderBy(e => e.Number);
}