
using MongoDB.Bson.Serialization;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task new_message__insert_message__message_stored()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage();

    await InsertMessageAsync(messages, message);

    Assert.IsNotNull(await FindMessageById(messages.AsQueryable(), message.MessageId).SingleAsync());
  }

  [TestMethod]
  public async Task new_discriminated_messages__insert_messages__discriminated_messages_stored()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message1 = CreateTestMessage(0);
    var message2 = CreateTestMessage(true);

    BsonClassMap.RegisterClassMap<Message<int>>(MapMessageClassType);
    BsonClassMap.RegisterClassMap<Message<bool>>(MapMessageClassType);

    await InsertMessageAsync(messages, message1);
    await InsertMessageAsync(messages, message2);

    Assert.IsNotNull(await FindMessageById(messages.AsQueryable(), message1.MessageId).SingleAsync());
    Assert.IsNotNull(await FindMessageById(messages.AsQueryable(), message2.MessageId).SingleAsync());
  }
}