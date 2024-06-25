
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task messages__find_message_by_key__stored_message_with_key ()
  {
    var messages = GetMessageCollection(AgendaDatabase);
    var message = CreateTestMessage();

    await InsertMessage(messages, message);

    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
  }

 [TestMethod]
  public async Task parent_message_and_message__find_message_duplication__stored_duplicated_message ()
  {
    var messages = GetMessageCollection(AgendaDatabase);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);
    var messageIdempotency = CreateMessageIdempotency(parent, message.MessageType);

    await InsertMessage(messages, parent);
    await InsertMessage(messages, message);

    Assert.IsNotNull( await FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

 [TestMethod]
  public async Task parent_message_and_message__find_message_duplication_with_different_type__no_duplicated_message ()
  {
    var messages = GetMessageCollection(AgendaDatabase);
    var parent = CreateTestMessage();
    var messageIdempotency = CreateMessageIdempotency(parent, "other mesage type");

    await InsertMessage(messages, parent);

    Assert.IsNull( await FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

 [TestMethod]
  public void active_and_inactive_messages__find_message_active_messages__stored_active_messages ()
  {
    Message[] messages = [CreateTestMessage(isActive: true, messageDate: DateTime.UtcNow.AddSeconds(5)), CreateTestMessage(isActive: false), CreateTestMessage(isActive: true, messageDate: DateTime.UtcNow)];

    var actual = FindActiveMessages(messages.AsQueryable(), DateTime.UtcNow);
    AreEqual(actual, [messages[2]]);
  }

 [TestMethod]
  public void messages__get_messages_page__paged_messages ()
  {
    Message[] messages = [CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage()];

    var actual = GetMessagesPage(messages.AsQueryable(), 1, 2);
    AreEqual(actual, [messages[1], messages[2]]);
  }
}