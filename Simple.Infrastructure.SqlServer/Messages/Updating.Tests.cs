
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    using var dbContext = CreateAgendaContext();
    var message = CreateTestMessage(isActive: true);

    AddMessage(dbContext, message);
    await SaveChangesAndClearContext(dbContext);

    UpdateMessageIsActive(dbContext, message, false);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindMessageByKey (dbContext.Messages, message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }
}