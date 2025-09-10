
using MongoDB.Driver;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static Task ResumeMessagesMongoAsync (IMongoDatabase mongoDatabase, Channel<Message> messageQueue, TimeProvider timeProvider, ResumeMessagesOptions resumeMessagesOptions, ILogger logger, CancellationToken cancellationToken = default) =>
    ResumeMessagesAsync(GetMessageCollection(mongoDatabase).AsQueryable(), (message) => EnqueueMessage(messageQueue, message), (query, cancellationToken) => query.ToListAsync(cancellationToken), timeProvider, resumeMessagesOptions, logger, cancellationToken);
}