
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<IEnumerable<Message>> ResumeMessagesMongoAsync (IMongoDatabase agendaDatabase, Channel<Message> mongoQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDatabase);
    var startDate = GetMessageStartDate(timeProvider.GetUtcNow(), messageHandlerOptions.ResumeDelay);
    var activeMessages = await FindActiveMessages(messages.AsQueryable(), startDate).ToListAsync(cancellationToken); // time-ordered message id.

    return EnqueueMessages(mongoQueue, activeMessages);
  }
}