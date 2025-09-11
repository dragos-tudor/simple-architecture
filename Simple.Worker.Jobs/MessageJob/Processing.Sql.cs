
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  internal static Task ProcessMessageSqlAsync(
    AgendaContextFactory dbContextFactory,
    byte maxErrors,
    MessageJobOptions jobOptions,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  =>
    ProcessMessagesAsync(
      async (message, cancellationToken) =>
      {
        // process messages here
        using var dbContext = CreateAgendaContext(dbContextFactory);
        await FinalizeMessageSqlAsync(message, dbContext, cancellationToken);
      },
      (message, exception, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(dbContextFactory);
        return HandleMessageErrorSqlAsync(message!, exception, maxErrors, dbContext, cancellationToken);
      },
      (minDate, maxDate, batchSize, cancellationToken) =>
      {
        using var dbContext = CreateAgendaContext(dbContextFactory);
        return QueryPendingMessages(dbContext.Messages, minDate, maxDate, batchSize).ToListAsync(cancellationToken);
      },
      jobOptions,
      timeProvider,
      logger,
      cancellationToken
    );
}