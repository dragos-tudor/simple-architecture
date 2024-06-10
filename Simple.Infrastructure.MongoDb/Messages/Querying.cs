
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<Message> FindActiveMessages (IMongoQueryable<Message> query) =>
    query.Where(message => message.IsActive);

 public static IMongoQueryable<Message> FindMessageByKey (IMongoQueryable<Message> query, Guid messageId) =>
    query.Where(message => message.MessageId == messageId);

  public static IMongoQueryable<Message> FindMessageByParent (IMongoQueryable<Message> query, Guid parentId) =>
    query.Where(message => message.ParentId == parentId);

  public static IMongoQueryable<Message> FindMessages (IMongoQueryable<Message> query, int? pageSize, int? pageIndex) =>
    query.Page(pageSize ?? 0, pageIndex);
}