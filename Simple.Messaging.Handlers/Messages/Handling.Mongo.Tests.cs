
using MongoDB.Driver;

namespace Simple.Messaging.Handlers;

partial class HandlersTests
{
  [TestMethod]
  public async Task pending_message__handling_mongo_message_error__message_error_updated()
  {
    var messageColl = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage(isPending: true);

    await InsertMessageAsync(messageColl, message);
    await HandleMessageErrorMongoAsync(MongoDatabase, message, new Exception("abc"), 5);

    var actual = await FindMessageById(messageColl.AsQueryable(), message.MessageId).FirstAsync();
    Assert.Contains("abc", actual.ErrorMessage);
  }

  [TestMethod]
  public async Task max_error_retries_message__handling_mongo_message_error__message_finalized()
  {
    var messageColl = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage(isPending: true);
    SetMessageErrorCounter(message, 4);

    await InsertMessageAsync(messageColl, message);
    await HandleMessageErrorMongoAsync(MongoDatabase, message, new Exception("abc"), 5);

    var actual = await FindMessageById(messageColl.AsQueryable(), message.MessageId).FirstAsync();
    Assert.IsFalse(actual.IsPending);
  }
}