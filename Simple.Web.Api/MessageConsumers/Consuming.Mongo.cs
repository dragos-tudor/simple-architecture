
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static Task ConsumeMongoMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, IMongoDatabase agendaDb) =>
    ConsumeMessages(messageQueue, subscribers, async (message, cancellationToken) => {
      var messages = GetMessageCollection(agendaDb);
      await UpdateMessageIsActive(messages, message, false, cancellationToken);
    });

}