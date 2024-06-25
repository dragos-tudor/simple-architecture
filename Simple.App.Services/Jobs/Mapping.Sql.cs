
namespace Simple.App.Services;

partial class ServicesFuncs
{
  static Job MapJobActionSql (Job job, AgendaContextFactory agendaContextFactory, Channel<Message> sqlQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider) =>
    job.JobName switch {
      ResumeMessages => job with { JobAction = () => ResumeMessagesSqlAsync(agendaContextFactory, sqlQueue, messageHandlerOptions, timeProvider, CancellationToken.None) },
      _ => job
    };

  public static IEnumerable<Job> MapJobActionsSql (IEnumerable<Job> jobs, AgendaContextFactory agendaContextFactory, Channel<Message> sqlQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider) =>
    jobs.Select(job => MapJobActionSql(job, agendaContextFactory, sqlQueue, messageHandlerOptions, timeProvider));
}