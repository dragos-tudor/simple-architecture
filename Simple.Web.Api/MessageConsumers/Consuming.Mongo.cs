
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static Task ConsumeMongoMessages (Channel<Message> messageQueue, Subscriber<Message, Failure>[] subscribers, IMongoDatabase agendaDb, ILogger logger) =>
    ConsumeMessages(
      messageQueue,
      (message, cancellationToken) => HandleMessage(message, GetMessageType(message)!, subscribers, cancellationToken),
      (message, cancellationToken) => FinalizeMongoMessage(message, agendaDb, cancellationToken),
      logger
    );
}