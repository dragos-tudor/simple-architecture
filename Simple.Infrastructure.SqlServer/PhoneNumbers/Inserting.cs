
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertContactPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    UpdateEntity(dbContext, contact, (contact) => AddContactPhoneNumber(dbContext, contact, phoneNumber));
    return SaveChanges(dbContext, cancellationToken);
  }
}