
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task messages__find_message_by_key__stored_message_with_key ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage();

    await InsertMessageAsync(messages, message);

    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
  }

 [TestMethod]
  public async Task parent_message_and_message__find_message_duplication__stored_duplicated_message ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);
    var messageIdempotency = CreateMessageIdempotency(parent, message.MessageType);

    await InsertMessageAsync(messages, parent);
    await InsertMessageAsync(messages, message);

    Assert.IsNotNull( await FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

 [TestMethod]
  public async Task parent_message_and_message__find_message_duplication_with_different_type__no_duplicated_message ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var parent = CreateTestMessage();
    var messageIdempotency = CreateMessageIdempotency(parent, "other mesage type");

    await InsertMessageAsync(messages, parent);

    Assert.IsNull( await FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

 [TestMethod]
  public void messages__find_active_messages_between_dates__active_messages ()
  {
    var currentDate = DateTime.UtcNow;
    Message[] messages = [
      CreateMessage(new object(), messageDate: currentDate, isActive: false),
      CreateMessage(new object(), messageDate: currentDate),
      CreateMessage(new object(), messageDate: currentDate.AddSeconds(1)),
      CreateMessage(new object(), messageDate: currentDate.AddSeconds(2))
    ];

    var actual = FindActiveMessages(messages.AsQueryable(), currentDate, currentDate.AddSeconds(2));
    AreEqual(actual, [messages[2], messages[3]]);
  }

 [TestMethod]
  public void messages__find_messages_page__messages_page ()
  {
    Message[] messages = [CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage()];

    var actual = FindMessagesPage(messages.AsQueryable(), 1, 2);
    AreEqual(actual, [messages[1], messages[2]]);
  }
}