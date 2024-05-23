
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task new_message__add_message__message_stored ()
  {
    using var dbContext = CreateAgendaContext();
    var message = CreateTestMessage();

    AddMessage(dbContext, message);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindMessageById (dbContext.Messages, message.MessageId).SingleAsync();
    Assert.AreEqual(actual, message);
  }
}