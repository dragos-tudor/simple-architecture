
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<Message> GetMessagesPage (IMongoQueryable<Message> query, int? pageSize, int? pageIndex) =>
    query.Page(pageSize ?? 0, pageIndex);
}