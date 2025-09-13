
using Microsoft.EntityFrameworkCore;

namespace Simple.Messaging.Handlers;

partial class HandlersTests
{
  [TestMethod]
  public async Task pending_message__handling_sql_message_error__message_error_updated()
  {
    var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(isPending: true);

    await InsertMessageAsync(dbContext, message);
    await HandleMessageErrorSqlAsync(dbContext, message, new Exception("abc"), 5);

    var actual = await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync();
    Assert.Contains("abc", actual.ErrorMessage);
  }

  [TestMethod]
  public async Task max_error_retries_message__handling_sql_message_error__message_finalized()
  {
    var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(isPending: true);
    SetMessageErrorCounter(message, 4);

    await InsertMessageAsync(dbContext, message);
    await HandleMessageErrorSqlAsync(dbContext, message, new Exception("abc"), 5);

    var actual = await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync();
    Assert.IsFalse(actual.IsPending);
  }
}