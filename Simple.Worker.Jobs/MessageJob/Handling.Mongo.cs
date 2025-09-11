
using MongoDB.Driver;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  static async Task<Message> HandleMessageErrorMongoAsync(Message message, Exception exception, byte maxErrors, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(mongoDatabase);
    var isPending = ShouldRetryProcessMessage(message, maxErrors);
    var errorCounter = GetMessageErrorCounter(message) + 1;

    SetMessageErrorMessage(message, exception.ToString());
    SetMessageErrorCounter(message, (byte)errorCounter);
    SetMessageIsPending(message, isPending);

    await UpdateMessageErrorAsync(messages, message, cancellationToken);
    return message;
  }
}