
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertContactSqlAsync(AgendaContext dbContext, Contact contact, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    return SaveChangesAsync(dbContext, cancellationToken);
  }

  public static Task InsertContactAndMessageSqlAsync(AgendaContext dbContext, Contact contact, Message message, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    AddMessage(dbContext, message);
    return SaveChangesAsync(dbContext, cancellationToken);
  }
}