
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<IEnumerable<Message>> ResumeMessagesSqlAsync (AgendaContextFactory agendaContextFactory, Channel<Message> sqlQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    var messages = await FindActiveMessages(agendaContext.Messages.AsQueryable(), timeProvider.GetUtcNow().DateTime, messageHandlerOptions.ResumeDelay).ToListAsync(cancellationToken); // time-ordered message id.

    foreach(var message in RestoreMessages(messages))
      EnqueueMessage(sqlQueue, message);
    return messages!;
  }
}