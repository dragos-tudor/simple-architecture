
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task message__update_message_is_pending__message_updated()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage(isPending: true);

    await InsertMessageAsync(messages, message);

    message.IsPending = false;
    await UpdateMessageIsPendingAsync(messages, message);

    var actual = await FindMessageById(messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.IsPending, false);
  }

  [TestMethod]
  public async Task message__update_message_error_informations__message_updated()
  {
    var messages = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage();

    await InsertMessageAsync(messages, message);

    message.ErrorMessage = "error";
    message.ErrorCounter = 3;
    await UpdateMessageErrorAsync(messages, message);

    var actual = await FindMessageById(messages.AsQueryable(), message.MessageId).SingleAsync();
    Assert.AreEqual(actual.ErrorMessage, "error");
    Assert.AreEqual(actual.ErrorCounter!.Value, 3);
  }
}