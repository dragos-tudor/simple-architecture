
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static Job MapResumeMessagesJobActionSql (ResumeMessagesJob job, AgendaContextFactory agendaContextFactory, Channel<Message> sqlQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider) =>
    job with { JobAction = () => ResumeMessagesSqlAsync(agendaContextFactory, sqlQueue, messageHandlerOptions, timeProvider, CancellationToken.None) };
}