
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertMessage (IClientSessionHandle session, IMongoCollection<Message> coll, Message message, CancellationToken cancellationToken = default) =>
    InsertDocument(session, coll, message, default, cancellationToken);

  public static Task InsertMessage (IMongoCollection<Message> coll, Message message, CancellationToken cancellationToken = default) =>
    InsertDocument(coll, message, default, cancellationToken);
}