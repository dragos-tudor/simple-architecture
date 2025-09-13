
using Microsoft.EntityFrameworkCore;

namespace Simple.Worker;

partial class WorkerTests
{
  [TestMethod]
  public async Task run_sql_message_job()
  {
    var messagePayload = CreateContactCreatedEvent(GetRandomGuid(), GetRandomEmail(24));
    var message = CreateTestMessage(messagePayload, messageDate: DateTime.UtcNow.AddHours(-1), isPending: true);
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    await InsertMessageAsync(dbContext, message);

    await WaitUntilAsync(
      async () => !(await FindMessageById(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync()).IsPending,
      TimeSpan.FromMilliseconds(100),
      TimeSpan.FromSeconds(10)
    );
  }
}