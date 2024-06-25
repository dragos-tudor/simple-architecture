
using Microsoft.EntityFrameworkCore;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<IEnumerable<Message>> ResumeMessagesSqlAsync (AgendaContextFactory agendaContextFactory, Channel<Message> sqlQueue, MessageHandlerOptions messageHandlerOptions, TimeProvider timeProvider, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    var startDate = GetMessageStartDate(timeProvider.GetUtcNow(), messageHandlerOptions.ResumeDelay);
    var activeMessages = await FindActiveMessages(agendaContext.Messages.AsQueryable(), startDate).ToListAsync(cancellationToken); // time-ordering message ids.

    return EnqueueMessages(sqlQueue, activeMessages);
  }
}