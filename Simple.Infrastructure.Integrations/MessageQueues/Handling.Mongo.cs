
using MongoDB.Driver;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  static async Task HandleErrorMongoMessage (Message message, Exception exception, IMongoDatabase mongoDatabase, byte maxFailures, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(mongoDatabase);
    var isActiveMessage = IsActiveMessage(message, exception, maxFailures);
    var failureCounter = (byte)(GetMessageFailureCounter(message) + 1);

    await UpdateMessageFailureAsync(messages, message, exception.ToString(), failureCounter, isActiveMessage, cancellationToken);
  }
}