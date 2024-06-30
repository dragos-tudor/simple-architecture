
using Microsoft.EntityFrameworkCore;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static async Task ResumeMessagesSqlAsync (AgendaContextFactory sqlContextFactory, Channel<Message> messageQueue, TimeProvider timeProvider, ResumeMessagesOptions resumeMessagesOptions, ILogger logger, CancellationToken cancellationToken = default)
  {
    using var dbContext = await sqlContextFactory.CreateDbContextAsync(cancellationToken);
    await ResumeMessagesAsync(dbContext.Messages.AsQueryable(), (message) => HasMessageContent(message) && EnqueueMessage(messageQueue, RestoreMessage(message)), (query, cancellationToken) => query.ToListAsync(cancellationToken), timeProvider, resumeMessagesOptions, logger, cancellationToken);
  }
}