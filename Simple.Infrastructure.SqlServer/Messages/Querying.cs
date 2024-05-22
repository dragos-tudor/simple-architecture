
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Message> GetMessageById (IQueryable<Message> query, Guid messageId) =>
    query.Where(contact => contact.MessageId == messageId);

  public static IQueryable<Message> GetActiveMessages (IQueryable<Message> query) =>
    query.Where(contact => contact.IsActive);
}