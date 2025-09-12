
using MongoDB.Driver;

namespace Simple.Messaging.Handlers;

partial class HandlersTests
{
  [TestMethod]
  public async Task pending_message__finalize_mongo_message__message_finalized()
  {
    var messageColl = GetMessageCollection(MongoDatabase);
    var message = CreateTestMessage(isPending: true);

    await InsertMessageAsync(messageColl, message);
    await FinalizeMessageMongoAsync(MongoDatabase, message);

    var actual = await FindMessageById(messageColl.AsQueryable(), message.MessageId).FirstAsync();
    Assert.IsFalse(actual.IsPending);
  }
}