
namespace Simple.Api;

partial class ApiFuncs
{
  public static Task ProcessMessageSqlAsync(
    Channel<Message> messageQueue,
    string sqlConnectionString,
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
        using var dbContext = CreateAgendaContext(sqlConnectionString);

        if (message is Message<ContactCreatedEvent>)
          await HandleContactCreatedSqlAsync((Message<ContactCreatedEvent>)message, dbContext, mailServerOptions, timeProvider.GetUtcNow().DateTime, cancellationToken);

        await FinalizeMessageSqlAsync(dbContext, message, cancellationToken);
      },
      async (message, exception, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(sqlConnectionString);
        await HandleMessageErrorSqlAsync(dbContext, message!, exception, maxErrors, cancellationToken);
      },
      logger,
      cancellationToken
    );
}