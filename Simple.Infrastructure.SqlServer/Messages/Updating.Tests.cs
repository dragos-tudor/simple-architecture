
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    using var dbContext1 = CreateAgendaContext();
    var message = CreateTestMessage(isActive: true);

    AddMessage(dbContext1, message);
    await dbContext1.SaveChangesAsync();

    using var dbContext2 = CreateAgendaContext();
    UpdateMessageIsActive(dbContext2, message, false);
    await dbContext2.SaveChangesAsync();

    using var dbContext3 = CreateAgendaContext();
    var actual = await GetMessageById (dbContext3.Messages, message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }
}