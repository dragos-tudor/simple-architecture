
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var message = CreateTestMessage(isActive: true);

    await InsertMessage(dbContext, message);
    ClearChangeTracker(dbContext);

    await UpdateMessageIsActive(dbContext, message, false);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }

 [TestMethod]
  public async Task message__update_message_failure_informations__message_updated ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var message = CreateTestMessage(isActive: true);

    await InsertMessage(dbContext, message);
    ClearChangeTracker(dbContext);

    await UpdateMessageFailure(dbContext, message, "failure", 3);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.FailureMessage, "failure");
    Assert.AreEqual(actual.FailureCounter, (byte?)3);
  }
}