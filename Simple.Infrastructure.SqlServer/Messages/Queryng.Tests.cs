
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task messages__find_message_by_id__message_with_id()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage();

    AddMessage(dbContext, message);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, message);
  }

  [TestMethod]
  public async Task parent_message_and_message__find_message_duplication__duplicated_message()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var parent = CreateTestMessage();
    var message = CreateTestMessage(parentId: parent.MessageId);
    var messageIdempotency = CreateTestMessageIdempotency(parent.MessageId, message.MessageType);

    AddMessage(dbContext, parent);
    AddMessage(dbContext, message);
    await SaveChangesAsync(dbContext);

    Assert.IsNotNull(await FindMessageDuplication(dbContext.Messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
  }

  [TestMethod]
  public async Task parent_message_and_message__find_message_duplication_with_different_type__no_duplicated_message()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var parent = CreateTestMessage();
    var messageIdempotency = CreateTestMessageIdempotency(parent.MessageId, "other mesage type");

    AddMessage(dbContext, parent);
    await SaveChangesAsync(dbContext);

    Assert.IsNull(await FindMessageDuplication(dbContext.Messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync());
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