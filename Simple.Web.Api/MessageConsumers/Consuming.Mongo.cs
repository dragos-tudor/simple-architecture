
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task FinalizeMongoMessage (Message message, IMongoDatabase agendaDb, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDb);
    await UpdateMessageIsActive(messages, message, false, cancellationToken);
  }

  internal static Task ConsumeMongoMessages (Channel<Message> messageQueue, Subscriber<Message, Failure>[] subscribers, IMongoDatabase agendaDb) =>
    ConsumeMessages(
      messageQueue,
      (message, cancellationToken) => HandleMessage(message, GetMessageType(message)!, subscribers, cancellationToken),
      (message, cancellationToken) => FinalizeMongoMessage(message, agendaDb, cancellationToken),
      Logger
    );
}