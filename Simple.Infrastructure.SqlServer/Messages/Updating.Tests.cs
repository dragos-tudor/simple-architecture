
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task message__update_message__message_updated()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(isPending: true);

    AddMessage(dbContext, message);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    UpdateMessage(dbContext, message, (message) => message.IsPending = false);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsPending, false);
  }
}