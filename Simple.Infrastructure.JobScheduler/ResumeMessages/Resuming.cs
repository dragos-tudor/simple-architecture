
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  public static async Task ResumeMessagesAsync<TQueryable> (
    TQueryable query, ResumeMessage<Message> resumeMessage, QueryModels<Message, TQueryable> findMessages,
    TimeProvider timeProvider, ResumeMessagesOptions resumeMessagesOptions,
    ILogger logger, CancellationToken cancellationToken = default) where TQueryable: IQueryable<Message>
  {
    IEnumerable<Message> messages = [];
    var currentDate = timeProvider.GetUtcNow().UtcDateTime;
    var exclusiveMinDate = GetMinResumeDate(currentDate, resumeMessagesOptions);
    var inclusiveMaxDate = GetMaxResumeDate(currentDate, resumeMessagesOptions);
    var batchSize = resumeMessagesOptions.BatchSize;
    LogStartResumingMessages(logger, exclusiveMinDate, inclusiveMaxDate);

    while(true)
    {
      var resumableQuery = FindResumableMessages(query, GetLastMessageDate(messages) ?? exclusiveMinDate, inclusiveMaxDate, batchSize);
      messages = await findMessages(resumableQuery, cancellationToken);

      foreach(var message in messages) {
        LogResumeMessage(logger, message.MessageId, message.CorrelationId);
        resumeMessage(message);
      }

      if(!ExistMessages(messages)) break;
    }

    LogEndResumingMessages(logger);
  }
}