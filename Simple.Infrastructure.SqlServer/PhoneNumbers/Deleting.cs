using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static void DeletePhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) => SqlFuncs.DeleteEntity(dbContext, phoneNumber);
}