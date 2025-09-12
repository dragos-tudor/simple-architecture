
using Microsoft.EntityFrameworkCore;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  public static Task ProcessMessageSqlAsync(
    string sqlConnectionString,
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
        using var dbContext = CreateAgendaContext(sqlConnectionString);
        return QueryPendingMessages(dbContext.Messages.AsQueryable(), minDate, maxDate, batchSize).ToListAsync(cancellationToken);
      },
      async (message, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(sqlConnectionString);
        if (message is Message<ContactCreatedEvent>)
          await HandleContactCreatedSqlAsync((Message<ContactCreatedEvent>)message, dbContext, mailServerOptions, timeProvider.GetUtcNow().DateTime, cancellationToken);

        await FinalizeMessageSqlAsync(dbContext, message, cancellationToken);
      },
      (message, exception, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(sqlConnectionString);
        return HandleMessageErrorSqlAsync(dbContext, message!, exception, maxErrors, cancellationToken);
      },
      jobOptions,
      timeProvider,
      logger,
      cancellationToken
    );
}