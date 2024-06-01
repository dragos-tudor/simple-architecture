
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task InsertContactAndMessage (
    IMongoCollection<Contact> contacts,
    IMongoCollection<Message> messages,
    Contact contact,
    Message message,
    TransactionOptions? transactionOptions = default,
    CancellationToken cancellationToken = default)
  {
    var client = contacts.Database.Client;

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