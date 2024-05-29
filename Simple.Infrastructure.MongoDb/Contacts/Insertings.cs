
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task InsertContactAndMessage (
    IMongoCollection<Contact> contactColl,
    IMongoCollection<Message> messageColl,
    Contact contact,
    Message message,
    TransactionOptions? transactionOptions = default,
    CancellationToken cancellationToken = default)
  {
    using var session = await GetMongoClient(contactColl).StartSessionAsync(default, cancellationToken);
    await session.WithTransactionAsync (
      async (session, cancellationToken) => {
        await InsertContact(session, contactColl, contact, cancellationToken);
        await InsertMessage(session, messageColl, message, cancellationToken);
        return true;
      }, transactionOptions, cancellationToken);
  }
}