
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static PhoneNumber AddPhoneNumber(AgendaContext dbContext, PhoneNumber phoneNumber) => AddEntity(dbContext, phoneNumber);
}