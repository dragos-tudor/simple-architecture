
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  internal static PhoneNumber AddPhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) => AddEntity(dbContext, phoneNumber);

  internal static IEnumerable<PhoneNumber> AddPhoneNumbers (AgendaContext dbContext, IEnumerable<PhoneNumber> phoneNumbers)
  {
    foreach(var phoneNumber in phoneNumbers)
      AddPhoneNumber(dbContext, phoneNumber);
    return phoneNumbers;
  }
}