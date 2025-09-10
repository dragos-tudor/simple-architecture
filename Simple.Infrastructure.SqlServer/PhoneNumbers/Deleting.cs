
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static PhoneNumber DeletePhoneNumber(AgendaContext dbContext, PhoneNumber phoneNumber) => DeleteEntity(dbContext, phoneNumber);
}