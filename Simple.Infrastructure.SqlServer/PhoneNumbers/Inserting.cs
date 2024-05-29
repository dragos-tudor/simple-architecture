
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertContactAndPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    AddPhoneNumber(dbContext, contact, phoneNumber);
    return SaveChanges(dbContext, cancellationToken);
  }
}