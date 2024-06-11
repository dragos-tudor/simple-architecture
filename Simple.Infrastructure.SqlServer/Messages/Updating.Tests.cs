
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var message = CreateTestMessage(isActive: true);

    AddMessage(dbContext, message);
    await SaveChanges(dbContext);
    ClearChangeTracker(dbContext);

    await UpdateMessageIsActive(dbContext, message, false);
    var actual = await FindMessageByKey(dbContext.Messages, message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }
}