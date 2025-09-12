
using Microsoft.EntityFrameworkCore;

namespace Simple.Messaging.Handlers;

partial class HandlersTests
{
  [TestMethod]
  public async Task pending_message__finalize_sql_message__message_finalized()
  {
    var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(isPending: true);

    await InsertMessageAsync(dbContext, message);
    await FinalizeMessageSqlAsync(dbContext, message);

    var actual = await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync();
    Assert.IsFalse(actual.IsPending);
  }
}