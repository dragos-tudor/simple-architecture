using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static void AddPhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) => SqlFuncs.AddEntity(dbContext, phoneNumber);

  static void AddPhoneNumbers (AgendaContext dbContext, PhoneNumber[] phoneNumbers)
  {
    foreach(var phoneNumber in phoneNumbers)
      AddPhoneNumber(dbContext, phoneNumber);
  }
}