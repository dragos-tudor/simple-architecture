
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task messages__find_message_by_key__stored_message_with_key ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var message = CreateTestMessage();

    await InsertMessage(dbContext, message);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, message);
  }

 [TestMethod]
  public async Task messages__find_message_by_parent__stored_message_with_parent ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);

    await InsertMessage(dbContext, parent);
    await InsertMessage(dbContext, message);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByParent(dbContext.Messages.AsQueryable(), parent.MessageId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, message);
  }

 [TestMethod]
  public void active_and_inactive_messages__find_message_active_messages__stored_active_messages ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    Message[] messages = [CreateTestMessage(isActive: true), CreateTestMessage(isActive: false), CreateTestMessage(isActive: true)];

    var actual = FindActiveMessages(messages.AsQueryable());
    AreEqual(actual, [messages[0], messages[2]]);
  }

 [TestMethod]
  public void messages__get_messages_page__paged_messages ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    Message[] messages = [CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage()];

    var actual = GetMessagesPage(messages.AsQueryable(), 1, 2);
    AreEqual(actual, [messages[1], messages[2]]);
  }
}