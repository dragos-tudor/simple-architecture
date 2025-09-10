
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  internal static Task InsertContactSqlAsync(AgendaContext dbContext, Contact contact, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    return SaveChangesAsync(dbContext, cancellationToken);
  }

  static Task InsertContactAndMessageSqlAsync(AgendaContext dbContext, Contact contact, Message message, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    AddMessage(dbContext, message);
    return SaveChangesAsync(dbContext, cancellationToken);
  }
}