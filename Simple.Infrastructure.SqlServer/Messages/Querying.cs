
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static IQueryable<Message> FindActiveMessages (IQueryable<Message> query) =>
    query.Where(message => message.IsActive);

  public static IQueryable<Message> FindMessageByKey (IQueryable<Message> query, Guid messageId) =>
    query.Where(message => message.MessageId == messageId);

  public static IQueryable<Message> FindMessageByParent (IQueryable<Message> query, Guid parentId) =>
    query.Where(message => message.ParentId == parentId);
}