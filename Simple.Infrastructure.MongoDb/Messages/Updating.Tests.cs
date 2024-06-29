
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage(isActive: true);

    await InsertMessageAsync(messages, message);
    await UpdateMessageIsActiveAsync(messages, message, false);

    var actual = await FindMessageByKey(messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }

 [TestMethod]
  public async Task message__update_message_failure_informations__message_updated ()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage(isActive: true);

    await InsertMessageAsync(messages, message);
    await UpdateMessageFailureAsync(messages, message, "failure", 3);

    var actual = await FindMessageByKey(messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.FailureMessage, "failure");
    Assert.AreEqual(actual.FailureCounter, (byte?)3);
  }
}