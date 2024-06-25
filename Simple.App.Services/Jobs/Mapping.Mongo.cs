
using MongoDB.Driver;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  static Job MapJobActionMongo (Job job, IMongoDatabase agendaDatabase, Channel<Message> mongoQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider) =>
    job.JobName switch {
      ResumeMessages => job with { JobAction = () => ResumeMessagesMongoAsync(agendaDatabase, mongoQueue, messageHandlerOptions, timeProvider, CancellationToken.None) },
      _ => job
    };

  public static IEnumerable<Job> MapJobActionsMongo (IEnumerable<Job> jobs, IMongoDatabase agendaDatabase, Channel<Message> mongoQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider) =>
    jobs.Select(job => MapJobActionMongo(job, agendaDatabase, mongoQueue, messageHandlerOptions, timeProvider));
}