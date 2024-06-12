
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task DeleteContactPhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    DeletePhoneNumber(dbContext, phoneNumber);
    return SaveChanges(dbContext, cancellationToken);
  }

  public static PhoneNumber DeletePhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) => DeleteEntity(dbContext, phoneNumber);
}