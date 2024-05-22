
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task message__add_message__message_stored ()
  {
    using var dbContext1 = CreateAgendaContext();
    var message = CreateTestMessage();

    AddMessage(dbContext1, message);
    await dbContext1.SaveChangesAsync();

    using var dbContext2 = CreateAgendaContext();
    var actual = await GetMessageById (dbContext2.Messages, message.MessageId).SingleAsync();
    Assert.AreEqual(actual, message);
  }
}