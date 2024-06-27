
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
  public async Task parent_message_and_message__find_message_duplication__stored_duplicated_message ()
  {
     using var dbContext = CreateAgendaContext(AgendaConnString);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);
    var messageIdempotency = CreateMessageIdempotency(parent, message.MessageType);

    await InsertMessage(dbContext, parent);
    await InsertMessage(dbContext, message);

    Assert.IsNotNull( await FindMessageDuplication(dbContext.Messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

 [TestMethod]
  public async Task parent_message_and_message__find_message_duplication_with_different_type__no_duplicated_message ()
  {
     using var dbContext = CreateAgendaContext(AgendaConnString);
    var parent = CreateTestMessage();
    var messageIdempotency = CreateMessageIdempotency(parent, "other mesage type");

    await InsertMessage(dbContext, parent);

    Assert.IsNull( await FindMessageDuplication(dbContext.Messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

 [TestMethod]
  public void active_and_inactive_messages__find_active_messages__active_messages ()
  {
    Message[] messages = [CreateMessage(new object(), isActive: true), CreateMessage(new object(), isActive: false), CreateMessage(new object(), isActive: true)];

    var actual = FindActiveMessages(messages.AsQueryable(), DateTime.UtcNow.AddSeconds(5));
    AreEqual(actual, [messages[0], messages[2]]);
  }

 [TestMethod]
  public void active_and_inactive_messages__find_active_messages_with_date_delay__active_messages ()
  {
    Message[] messages = [CreateMessage(new object(), isActive: true), CreateMessage(new object(), isActive: false)];

    var actual = FindActiveMessages(messages.AsQueryable(), DateTime.UtcNow, TimeSpan.FromSeconds(-1));
    AreEqual(actual, [messages[0]]);
  }

 [TestMethod]
  public void old_and_new_messages__find_active_messages_with_date_delay__old_messages ()
  {
    Message[] messages = [CreateMessage(new object(), messageDate: DateTime.UtcNow.AddSeconds(-2), isActive: true), CreateMessage(new object(), isActive: true)];

    var actual = FindActiveMessages(messages.AsQueryable(), DateTime.UtcNow, TimeSpan.FromSeconds(1));
    AreEqual(actual, [messages[0]]);
  }

 [TestMethod]
  public void messages__find_messages_page__messages_page ()
  {
    Message[] messages = [CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage(), CreateTestMessage()];

    var actual = FindMessagesPage(messages.AsQueryable(), 1, 2);
    AreEqual(actual, [messages[1], messages[2]]);
  }
}