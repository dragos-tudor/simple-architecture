
namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  static async Task HandleErrorSqlMessage (Message message, Exception exception, AgendaContextFactory sqlContextFactory, byte maxFailures, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync(cancellationToken);
    var isActiveMessage = IsActiveMessage(message, exception, maxFailures);
    var failureCounter = (byte)(GetMessageFailureCounter(message) + 1);

    await UpdateMessageFailureAsync(agendaContext, message, exception.ToString(), failureCounter, isActiveMessage, cancellationToken);
  }
}