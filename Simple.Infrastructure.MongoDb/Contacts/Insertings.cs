
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<Guid> InsertContactAndMessage (
    IMongoCollection<Contact> contacts,
    IMongoCollection<Message> messages,
    Contact contact,
    Message message,
    TransactionOptions? transactionOptions = default,
    CancellationToken cancellationToken = default)
  {
    var client = contacts.Database.Client;
    using var session = await client.StartSessionAsync(default, cancellationToken);

    return await session.WithTransactionAsync (
      async (session, cancellationToken) => {
        await InsertContact(session, contacts, contact, cancellationToken);
        await InsertMessage(session, messages, message, cancellationToken);
        return contact.ContactId;
      },
      transactionOptions,
      cancellationToken
    );
  }
}