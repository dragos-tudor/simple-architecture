
using MongoDB.Driver;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  static async Task HandleErrorMongoMessage (Message message, Exception exception, IMongoDatabase agendaDb, MessageHandlerOptions handlerOptions, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDb);
    var isActiveMessage = IsActiveMessage(message, exception, handlerOptions);
    var failureCounter = (byte)(GetMessageFailureCounter(message) + 1);

    await UpdateMessageFailure(messages, message, exception.ToString(), failureCounter, isActiveMessage, cancellationToken);
  }
}