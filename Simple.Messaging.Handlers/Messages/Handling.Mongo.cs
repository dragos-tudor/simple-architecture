
using MongoDB.Driver;

namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> HandleMessageErrorMongoAsync(IMongoDatabase mongoDatabase, Message message, Exception exception, byte maxErrors, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(mongoDatabase);
    var errorCounter = GetMessageErrorCounter(message) + 1;

    SetMessageErrorMessage(message, exception.ToString());
    SetMessageErrorCounter(message, (byte)errorCounter);
    SetMessageIsPending(message, ShouldRetryProcessMessage(message, maxErrors));

    await UpdateMessageErrorAsync(messages, message, cancellationToken);
    return message;
  }
}