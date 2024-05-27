
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static PhoneNumber DeletePhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) =>
    SqlFuncs.DeleteEntity(dbContext, phoneNumber);
}