
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
      async () => await FindMessagesByCorrelationId(dbContext.Messages.AsQueryable(), message.CorrelationId!).CountAsync() == 2,
      TimeSpan.FromMilliseconds(500),
      TimeSpan.FromSeconds(10)
    );
  }
}