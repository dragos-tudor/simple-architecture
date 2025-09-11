
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertPhoneNumberSqlAsync(AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    AddPhoneNumber(dbContext, phoneNumber);
    UpdateContact(dbContext, contact);
    return SaveChangesAsync(dbContext, cancellationToken);
  }
}