
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task messages__find_message_by_id__message_with_id()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage();

    await InsertMessageAsync(messages, message);

    Assert.IsNotNull(await FindMessageById(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
  }

  [TestMethod]
  public async Task parent_message_and_message__find_message_duplication__duplicated_message()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);
    var messageIdempotency = CreateTestMessageIdempotency(parent.MessageId, message.MessageType);

    await InsertMessageAsync(messages, parent);
    await InsertMessageAsync(messages, message);

    Assert.IsNotNull(await FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

  [TestMethod]
  public async Task parent_message_and_message__find_message_duplication_with_different_type__no_duplicated_message()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var parent = CreateTestMessage();
    var messageIdempotency = CreateTestMessageIdempotency(parent.MessageId, "other mesage type");

    await InsertMessageAsync(messages, parent);

    Assert.IsNull(await FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

  [TestMethod]
  public void messages__query_pending_messages_between_dates__pending_messages()
  {
    var currentDate = DateTime.UtcNow;
    Message[] messages = [
      CreateTestMessage(messageDate: currentDate, isPending: true),
      CreateTestMessage(messageDate: currentDate, isPending: true),
      CreateTestMessage(messageDate: currentDate.AddSeconds(1), isPending: true),
      CreateTestMessage(messageDate: currentDate.AddSeconds(2), isPending: true)
    ];

    var actual = QueryPendingMessages(messages.AsQueryable(), currentDate, currentDate.AddSeconds(2));
    AreEqual(actual, [messages[2], messages[3]]);
  }

  [TestMethod]
  public void messages__find_messages_page__messages_page()
  {
    Message[] messages = [CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage()];

    var actual = FindMessagesPage(messages.AsQueryable(), 1, 2);
    AreEqual(actual, [messages[1], messages[2]]);
  }
}