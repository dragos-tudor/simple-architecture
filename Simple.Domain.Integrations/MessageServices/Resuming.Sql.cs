
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  internal static async Task<IEnumerable<Message>> ResumeMessagesPageSqlAsync (AgendaContextFactory sqlContextFactory, Channel<Message> messageQueue, DateTime minDate, DateTime maxDate, int batchSize, CancellationToken cancellationToken = default)
  {
    using var dbContext = await sqlContextFactory.CreateDbContextAsync(cancellationToken);
    var messages = await FindResumableMessages(dbContext.Messages.AsQueryable(), minDate, maxDate, batchSize).ToListAsync(cancellationToken);

    foreach(var message in messages)
      EnqueueMessage(messageQueue, message);
    return messages;
  }

  public static async Task ResumeMessagesSqlAsync (AgendaContextFactory sqlContextFactory, Channel<Message> messageQueue, TimeProvider timeProvider, ResumeMessagesOptions resumeMessagesOptions, CancellationToken cancellationToken = default)
  {
    IEnumerable<Message> messages = [];
    var currentDate = timeProvider.GetUtcNow().UtcDateTime;
    var minDate = GetMinResumeDate(currentDate, resumeMessagesOptions);
    var maxDate = GetMaxResumeDate(currentDate, resumeMessagesOptions);
    var batchSize = resumeMessagesOptions.BatchSize;

    do { messages = await ResumeMessagesPageSqlAsync(sqlContextFactory, messageQueue, GetLastMessageDate(messages) ?? minDate, maxDate, batchSize, cancellationToken); }
    while (messages.Any());
  }
}