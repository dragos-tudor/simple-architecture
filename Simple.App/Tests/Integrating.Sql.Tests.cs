
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Simple.App;

partial class AppTests
{
  [TestMethod]
  public async Task resume_messages_job_sql_tests ()
  {
    var configuration = BuildConfiguration("settings.tests.json");
    using var loggerFactory = NullLoggerFactory.Instance; // IntegrateSerilog(configuration);

    var timeProvider = new FakeTimeProvider();
    var contact = CreateTestContact();
    var message = CreateMessage(new ContactCreatedEvent(contact.ContactId, contact.ContactEmail), messageDate: timeProvider.GetUtcNow().UtcDateTime);
    using var dbContext = await ServerIntegrations.SqlIntegration.SqlContextFactory.CreateDbContextAsync();
    await InsertMessageAsync(dbContext, message);

    var job = ResolveResumeMessagesSqlJob(ServerIntegrations.SqlIntegration, configuration, timeProvider, loggerFactory);
    using var jobScheduler = IntegrateJobScheduler([job], configuration, timeProvider, loggerFactory);

    await WaitUntilAsync(async () => !(await FindMessageByKey(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync()).IsActive, TimeSpan.FromMilliseconds(100));
  }
}