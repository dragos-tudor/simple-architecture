
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task HandleErrorMongoMessage (Message message, Exception exception, IMongoDatabase agendaDb, MessageHandlerOptions handlerOptions, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDb);
    var isActiveMessage = IsMessageActive(message, handlerOptions);
    var failureCounter = (byte)(GetMessageFailureCounter(message) + 1);

    await UpdateMessageFailure(messages, message, exception.ToString(), failureCounter, isActiveMessage, cancellationToken);
  }
}