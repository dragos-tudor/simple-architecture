
using Microsoft.EntityFrameworkCore;

namespace Simple.Worker;

partial class WorkerTests
{
  [TestMethod]
  public async Task run_sql_message_job()
  {
    var contact = CreateTestContact();
    var messagePayload = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateTestMessage(messagePayload, messageDate: DateTime.UtcNow.AddHours(-1), isPending: true);
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    await InsertMessageAsync(dbContext, message);

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    await WaitUntilAsync(
      async () => !(await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync()).IsPending,
      TimeSpan.FromMilliseconds(100),
      cancellationTokenSource.Token);
  }
}