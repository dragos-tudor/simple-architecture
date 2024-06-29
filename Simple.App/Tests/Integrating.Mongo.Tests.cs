
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Simple.App;

partial class AppTests
{
  [TestMethod]
  public async Task resume_messages_job_mongo_tests ()
  {
    var configuration = BuildConfiguration("settings.tests.json");
    using var loggerFactory = NullLoggerFactory.Instance; // IntegrateSerilog(configuration);

    var timeProvider = new FakeTimeProvider();
    var contact = CreateTestContact();
    var message = CreateMessage(new ContactCreatedEvent(contact.ContactId, contact.ContactEmail), messageDate: timeProvider.GetUtcNow().UtcDateTime);
    var messages = GetMessageCollection(ServerIntegrations.MongoIntegration.MongoDatabase);
    await InsertMessageAsync(messages, message);

    var job = ResolveResumeMessagesMongoJob(ServerIntegrations.MongoIntegration, configuration, timeProvider, loggerFactory);
    using var jobScheduler = IntegrateJobScheduler([job], configuration, timeProvider, loggerFactory);

    await WaitUntilAsync(async () => !(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstAsync()).IsActive, TimeSpan.FromMilliseconds(100));
  }
}