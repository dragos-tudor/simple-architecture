
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task message__update_message_is_active__message_updated ()
  {
    var messages = GetMessageCollection(Database);
    var message = CreateTestMessage(isActive: true);

    await InsertMessage(messages, message);

    await UpdateMessageIsActive(messages, message, false);

    var actual = await FindMessageByKey (messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsActive, false);
  }
}