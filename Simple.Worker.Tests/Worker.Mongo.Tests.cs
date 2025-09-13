
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Simple.Worker;

partial class WorkerTests
{
  [TestMethod]
  public async Task run_mongo_message_job()
  {
    var messagePayload = CreateContactCreatedEvent(GetRandomGuid(), GetRandomEmail(24));
    var message = CreateTestMessage(messagePayload, messageDate: DateTime.UtcNow.AddHours(-1), isPending: true);
    var messageColl = GetMessageCollection(MongoDatabase);

    BsonClassMap.RegisterClassMap<Message<ContactCreatedEvent>>(MapMessageClassType);
    await InsertMessageAsync(messageColl, message);

    await WaitUntilAsync(
      async () => !(await FindMessageById(messageColl.AsQueryable(), message.MessageId).FirstAsync()).IsPending,
      TimeSpan.FromMilliseconds(200),
      TimeSpan.FromSeconds(10)
    );
  }
}