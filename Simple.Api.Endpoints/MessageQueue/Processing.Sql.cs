
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static Task ProcessMessageSqlAsync(
    Channel<Message> messageQueue,
    AgendaContextFactory dbContextFactory,
    byte maxErrors,
    MailServerOptions mailServerOptions,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  =>
    ProcessMessagesAsync(
      messageQueue,
      async (message, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(dbContextFactory);
        if (message is Message<ContactCreatedEvent>)
          await HandleContactCreatedSqlAsync((Message<ContactCreatedEvent>)message, dbContext, mailServerOptions, timeProvider.GetUtcNow().DateTime, cancellationToken);
        await FinalizeMessageSqlAsync(dbContext, message, cancellationToken);
      },
      async (message, exception, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(dbContextFactory);
        await HandleMessageErrorSqlAsync(dbContext, message!, exception, maxErrors, cancellationToken);
      },
      logger,
      cancellationToken
    );
}