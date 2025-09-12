
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Simple.Worker;

partial class WorkerTests
{
  [TestMethod]
  public async Task run_mongo_message_job()
  {
    var contact = CreateTestContact();
    var messagePayload = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateTestMessage(messagePayload, messageDate: DateTime.UtcNow.AddHours(-1), isPending: true);
    var messageColl = GetMessageCollection(MongoDatabase);

    BsonClassMap.RegisterClassMap<Message<ContactCreatedEvent>>(MapMessageClassType);
    await InsertMessageAsync(messageColl, message);

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    await WaitUntilAsync(
      async () => !(await FindMessageById(messageColl.AsQueryable(), message.MessageId).FirstAsync()).IsPending,
      TimeSpan.FromMilliseconds(100),
      cancellationTokenSource.Token);
  }
}