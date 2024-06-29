
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  [LoggerMessage(1, LogLevel.Information, "JobScheduler: start resuming messages from [{MinDate}] to [{MaxDate}].", EventName = "LogStartResumingMessages")]
  public static partial void LogStartResumingMessages (ILogger logger, DateTime minDate, DateTime maxDate);

  [LoggerMessage(2, LogLevel.Information, "JobScheduler: end resuming messages.", EventName = "LogEndResumingMessages")]
  public static partial void LogEndResumingMessages (ILogger logger);

  [LoggerMessage(3, LogLevel.Information, "JobScheduler: resume message [{MessageId}]. [{CorrelationId}]", EventName = "LogResumeMessage")]
  public static partial void LogResumeMessage (ILogger logger, Guid messageId, string? correlationId);
}