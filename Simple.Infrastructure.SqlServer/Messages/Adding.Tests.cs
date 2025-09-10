
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task new_message__add_message__message_stored()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage();

    AddMessage(dbContext, message);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual, message);
  }
}