
using Microsoft.EntityFrameworkCore;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  public static Task ProcessMessageSqlAsync(
    AgendaContextFactory dbContextFactory,
    byte maxErrors,
    MessageJobOptions jobOptions,
    MailServerOptions mailServerOptions,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  =>
    ProcessMessagesAsync(
      (minDate, maxDate, batchSize, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(dbContextFactory);
        return QueryPendingMessages(dbContext.Messages, minDate, maxDate, batchSize).ToListAsync(cancellationToken);
      },
      async (message, cancellationToken) =>
      {

        using var dbContext = CreateAgendaContext(dbContextFactory);
        if (message is Message<ContactCreatedEvent>)
          await HandleContactCreatedSqlAsync((Message<ContactCreatedEvent>)message, dbContext, mailServerOptions, timeProvider.GetUtcNow().DateTime, cancellationToken);
        await FinalizeMessageSqlAsync(dbContext, message, cancellationToken);
      },
      (message, exception, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(dbContextFactory);
        return HandleMessageErrorSqlAsync(dbContext, message!, exception, maxErrors, cancellationToken);
      },
      jobOptions,
      timeProvider,
      logger,
      cancellationToken
    );
}