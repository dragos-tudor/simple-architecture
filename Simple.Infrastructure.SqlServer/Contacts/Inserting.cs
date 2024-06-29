
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertContactAsync (AgendaContext dbContext, Contact contact, CancellationToken cancellationToken = default)
  {
    AddEntity(dbContext, contact);
    AddEntities(dbContext, contact.PhoneNumbers ?? []);
    return SaveChanges(dbContext, cancellationToken);
  }

  public static Task InsertContactAndMessageAsync (AgendaContext dbContext, Contact contact, Message message, CancellationToken cancellationToken = default)
  {
    AddEntity(dbContext, contact);
    AddEntities(dbContext, contact.PhoneNumbers ?? []);
    AddEntity(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }
}