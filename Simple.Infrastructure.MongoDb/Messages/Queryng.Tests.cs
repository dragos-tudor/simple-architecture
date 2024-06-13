
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task messages__find_message_by_key__stored_message_with_key ()
  {
    var messages = GetMessageCollection(Database);
    var message = CreateTestMessage();

    await InsertMessage(messages, message);

    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync());
  }

 [TestMethod]
  public async Task messages__find_message_by_parent__stored_message_with_parent ()
  {
    var messages = GetMessageCollection(Database);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);

    await InsertMessage(messages, parent);
    await InsertMessage(messages, message);

    Assert.IsNotNull( await FindMessageByParent(messages.AsQueryable(), parent.MessageId).FirstOrDefaultAsync());
  }

 [TestMethod]
  public void active_and_inactive_messages__find_message_active_messages__stored_active_messages ()
  {
    Message[] messages = [CreateTestMessage(isActive: true), CreateTestMessage(isActive: false), CreateTestMessage(isActive: true)];

    var actual = FindActiveMessages(messages.AsQueryable());
    AreEqual(actual, [messages[0], messages[2]]);
  }

 [TestMethod]
  public void messages__get_messages_page__paged_messages ()
  {
    Message[] messages = [CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage()];

    var actual = GetMessagesPage(messages.AsQueryable(), 1, 2);
    AreEqual(actual, [messages[1], messages[2]]);
  }
}