
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task new_message__insert_message__message_stored ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage();

    await InsertMessageAsync(messages, message);

    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).SingleAsync());
  }

 [TestMethod]
  public async Task new_discriminated_messages__insert_messages__discriminated_messages_stored ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message1 = CreateMessage(CreateAddedToAgendaNotification("", ""));
    var message2 = CreateMessage(CreateRemovedFromAgendaNotification("", ""));

    await InsertMessageAsync(messages, message1);
    await InsertMessageAsync(messages, message2);

    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message1.MessageId).SingleAsync());
    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message2.MessageId).SingleAsync());
  }
}