
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertContactAndMessage (AgendaContext dbContext, Contact contact, Message message, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    AddMessage(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }
}