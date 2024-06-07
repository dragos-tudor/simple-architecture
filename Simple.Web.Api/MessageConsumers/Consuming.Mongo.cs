
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static Task ConsumeMongoMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, IMongoDatabase agendaDb) =>
    ConsumeMessages(messageQueue,
      (message, cancellationToken) =>
        ConsumeMessage(
          message, (message, cancellationToken) => HandleMessage(message, GetMessageType(message)!, subscribers, cancellationToken), cancellationToken),
      async (message, cancellationToken) => {
        var messages = GetMessageCollection(agendaDb);
        await UpdateMessageIsActive(messages, message, false, cancellationToken);
    });
}