
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task DeletePhoneNumberAsync (AgendaContext dbContext, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    DeleteEntity(dbContext, phoneNumber);
    return SaveChanges(dbContext, cancellationToken);
  }
}