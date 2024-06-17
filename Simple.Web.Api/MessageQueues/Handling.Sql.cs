
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task HandleErrorSqlMessage (Message message, Exception exception, AgendaContextFactory agendaContextFactory, MessageHandlerOptions handlerOptions, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    var isActiveMessage = IsMessageActive(message, handlerOptions);
    var failureCounter = (byte)(GetMessageFailureCounter(message) + 1);

    await UpdateMessageFailure(agendaContext, message, exception.ToString(), failureCounter, isActiveMessage, cancellationToken);
  }
}