
using MongoDB.Driver;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  static async Task<Message> FinalizeMessageMongoAsync(Message message, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(mongoDatabase);
    SetMessageIsPending(message, false);

    await UpdateMessageIsPendingAsync(messages, message, cancellationToken);
    return message;
  }
}