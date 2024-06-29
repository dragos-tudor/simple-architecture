
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertPhoneNumberAsync (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    UpdateEntity(dbContext, contact, (contact) => {
      SetContactPhoneNumber(contact, phoneNumber);
      AddEntity(dbContext, phoneNumber);
    });
    return SaveChanges(dbContext, cancellationToken);
  }
}