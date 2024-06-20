
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task new_message__insert_message__message_stored ()
  {
    var messages = GetMessageCollection(AgendaDb);
    var message = CreateTestMessage();

    await InsertMessage(messages, message);

    Assert.IsNotNull(await FindMessageByKey(messages.AsQueryable(), message.MessageId).SingleAsync());
  }
}