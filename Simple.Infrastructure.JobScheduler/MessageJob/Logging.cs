
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  [LoggerMessage(1, LogLevel.Information, "JobScheduler: start process messages from [{MinDate}] to [{MaxDate}].", EventName = "LogStartProcessMessages")]
  public static partial void LogStartProcessMessages(ILogger logger, DateTime minDate, DateTime maxDate);

  [LoggerMessage(1, LogLevel.Information, "JobScheduler: end process messages from [{MinDate}] to [{MaxDate}].", EventName = "LogEndProcessMessages")]
  public static partial void LogEndProcessMessages(ILogger logger, DateTime minDate, DateTime maxDate);

  [LoggerMessage(3, LogLevel.Information, "JobScheduler: process message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]", EventName = "LogProcessMessage")]
  public static partial void LogProcessMessage(ILogger logger, Guid? messageId, string? messageType, string? correlationId);

  [LoggerMessage(11, LogLevel.Error, "JobScheduler: process error [{Error}] for message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]\n{StackTrace}", EventName = "LogProcessMessageError")]
  public static partial void LogProcessMessageError(ILogger logger, Guid? messageId, string? messageType, string? correlationId, string error, string? stackTrace);

  [LoggerMessage(12, LogLevel.Error, "JobScheduler: find messages error [{Error}]. [{StackTrace}", EventName = "LogFindMessagesError")]
  public static partial void LogFinfMessagesError(ILogger logger, string error, string? stackTrace);

}