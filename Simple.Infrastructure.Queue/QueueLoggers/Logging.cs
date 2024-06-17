
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Consumer: handling message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]", EventName = "LogHandlingMessage")]
  public static partial void LogHandlingMessage (ILogger logger, Guid? messageId, string? messageType, string? correlationId);

  [LoggerMessage(2, LogLevel.Error, "Consuner: start consuming messages.", EventName = "LogStartConsumingMessages")]
  public static partial void LogStartConsumingMessages (ILogger logger);

  [LoggerMessage(3, LogLevel.Error, "Consuner: end consuming messages.", EventName = "LogEndConsumingMessages")]
  public static partial void LogEndConsumingMessages (ILogger logger);

  [LoggerMessage(11, LogLevel.Error, "Consumer: handled error [{Error}] for message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]\n{StackTrace}", EventName = "LogHandlingMessageError")]
  public static partial void LogHandledMessageError (ILogger logger, Guid? messageId, string? messageType, string? correlationId, string error, string? stackTrace);

  [LoggerMessage(12, LogLevel.Error, "Consuner: handling error [{Error}] for message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]\n{StackTrace}", EventName = "LogConsumingMessageError")]
  public static partial void LogHandlingMessageError (ILogger logger, Guid? messageId, string? messageType, string? correlationId, string error, string? stackTrace);
}