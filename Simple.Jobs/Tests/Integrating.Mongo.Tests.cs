
using MongoDB.Driver;

namespace Simple.Jobs;

partial class JobsTests
{
  [TestMethod]
  public async Task resume_messages_job_mongo_tests ()
  {
    var contact = CreateTestContact();
    var message = CreateMessage(new ContactCreatedEvent(contact.ContactId, contact.ContactEmail));
    var messages = GetMessageCollection(ServerIntegrations.MongoIntegration.MongoDatabase);
    await InsertMessageAsync(messages, message);

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    await WaitUntilAsync(async () => !(await FindMessageByKey(messages.AsQueryable(), message.MessageId).FirstAsync()).IsActive, TimeSpan.FromMilliseconds(100), cancellationTokenSource.Token);
  }
}