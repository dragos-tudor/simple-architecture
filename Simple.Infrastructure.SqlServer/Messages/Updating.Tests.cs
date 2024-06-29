
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(isActive: true);

    await InsertMessageAsync(dbContext, message);
    ClearChangeTracker(dbContext);

    await UpdateMessageIsActiveAsync(dbContext, message, false);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }

 [TestMethod]
  public async Task message__update_message_failure_informations__message_updated ()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(isActive: true);

    await InsertMessageAsync(dbContext, message);
    ClearChangeTracker(dbContext);

    await UpdateMessageFailureAsync(dbContext, message, "failure", 3);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.FailureMessage, "failure");
    Assert.AreEqual(actual.FailureCounter, (byte?)3);
  }
}