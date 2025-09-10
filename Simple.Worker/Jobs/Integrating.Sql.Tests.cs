
using Microsoft.EntityFrameworkCore;

namespace Simple.Worker;

partial class WorkerTests
{
  [TestMethod]
  public async Task process_messages_sql_job()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(new ContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    await InsertMessageAsync(dbContext, message);

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    await WaitUntilAsync(async () => !(await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync()).IsPending, TimeSpan.FromMilliseconds(100), cancellationTokenSource.Token);
  }
}