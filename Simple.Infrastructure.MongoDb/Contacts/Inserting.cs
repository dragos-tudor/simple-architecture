
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertContact (
    IClientSessionHandle session,
    IMongoCollection<Contact> coll,
    Contact contact,
    CancellationToken cancellationToken = default) =>
      InsertDocument(session, coll, contact, default, cancellationToken);
}