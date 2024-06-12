
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<Contact> GetContactsPage (IMongoQueryable<Contact> query, int? pageSize, int? pageIndex) =>
    query.Page(pageSize ?? 0, pageIndex);
}