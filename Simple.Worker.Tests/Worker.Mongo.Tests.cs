
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
      async () => await FindMessagesByCorrelationId(messageColl.AsQueryable(), message.CorrelationId!).CountAsync() == 2,
      TimeSpan.FromMilliseconds(500),
      TimeSpan.FromSeconds(10)
    );
  }
}