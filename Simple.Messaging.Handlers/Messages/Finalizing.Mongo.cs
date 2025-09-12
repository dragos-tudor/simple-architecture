
using MongoDB.Driver;

namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> FinalizeMessageMongoAsync(Message message, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(mongoDatabase);
    SetMessageIsPending(message, false);

    await UpdateMessageIsPendingAsync(messages, message, cancellationToken);
    return message;
  }
}