
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<Message> FindMessageByKey (IMongoQueryable<Message> query, Guid messageId) =>
    query.Where(contact => contact.MessageId == messageId);
}