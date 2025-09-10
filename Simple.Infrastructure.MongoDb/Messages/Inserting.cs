
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertMessageAsync(IMongoCollection<Message> coll, Message message, CancellationToken cancellationToken = default) =>
    InsertDocument(coll, message, default, cancellationToken);
}