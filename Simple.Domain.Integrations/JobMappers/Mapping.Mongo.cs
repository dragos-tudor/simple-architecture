
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static Job MapResumeMessagesJobActionMongo (ResumeMessagesJob job, IMongoDatabase agendaDatabase, Channel<Message> mongoQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider) =>
    job with { JobAction = () => ResumeMessagesMongoAsync(agendaDatabase, mongoQueue, messageHandlerOptions, timeProvider, CancellationToken.None) };
}