
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task InsertContactAndMessage (
    IMongoClient client,
    Contact contact,
    Message message,
    TransactionOptions? transactionOptions = default,
    CancellationToken cancellationToken = default)
  {
    var db = GetMongoDatabase(client);
    var contacts = GetContactCollection(db);
    var messages = GetMessageCollection(db);

    using var session = await client.StartSessionAsync(default, cancellationToken);
    try {
      session.StartTransaction(transactionOptions);
      await InsertContact(session, contacts, contact, cancellationToken);
      await InsertMessage(session, messages, message, cancellationToken);
    }
    catch(Exception) {
      await session.AbortTransactionAsync(cancellationToken);
      throw;
    }
    await session.CommitTransactionAsync(cancellationToken);
  }
}