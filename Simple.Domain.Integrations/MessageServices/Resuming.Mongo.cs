
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<IEnumerable<Message>> ResumeMessagesMongoAsync (IMongoDatabase agendaDatabase, Channel<Message> mongoQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider, CancellationToken cancellationToken = default)
  {
    var messageCollection = GetMessageCollection(agendaDatabase);
    var messages = await FindActiveMessages(messageCollection.AsQueryable(), timeProvider.GetUtcNow().DateTime, messageHandlerOptions.ResumeDelay).ToListAsync(cancellationToken); // time-ordered message id.

    foreach(var message in RestoreMessages(messages))
      EnqueueMessage(mongoQueue, message);
    return messages;
  }
}