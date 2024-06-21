
using MongoDB.Driver;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  static async Task HandleErrorMongoMessage (Message message, Exception exception, IMongoDatabase agendaDb, byte maxFailures, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDb);
    var isActiveMessage = IsActiveMessage(message, exception, maxFailures);
    var failureCounter = (byte)(GetMessageFailureCounter(message) + 1);

    await UpdateMessageFailure(messages, message, exception.ToString(), failureCounter, isActiveMessage, cancellationToken);
  }
}