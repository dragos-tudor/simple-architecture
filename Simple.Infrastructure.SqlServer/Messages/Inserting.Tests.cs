
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task new_message__add_message__message_stored ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var message = CreateTestMessage();

    await InsertMessage(dbContext, message);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual, message);
  }
}