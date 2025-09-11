
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  internal static Task ProcessMessageSqlAsync(Channel<Message> messageQueue, AgendaContextFactory sqlContextFactory, byte maxErrors, ILogger logger, CancellationToken cancellationToken = default) =>
      ProcessMessagesAsync(
        messageQueue,
        async (message, cancellationToken) =>
        {
          using var dbContext = CreateAgendaContext(sqlContextFactory);
          await FinalizeMessageSqlAsync(message, dbContext, cancellationToken);
        },
        async (message, exception, cancellationToken) =>
        {
          using var dbContext = CreateAgendaContext(sqlContextFactory);
          await HandleMessageErrorSqlAsync(message!, exception, maxErrors, dbContext, cancellationToken);
        },
        logger,
        cancellationToken
      );
}