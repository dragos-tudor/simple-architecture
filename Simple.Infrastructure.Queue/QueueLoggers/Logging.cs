
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Queue: handle message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]", EventName = "LogHandleMessage")]
  public static partial void LogHandleMessage (ILogger logger, Guid? messageId, string? messageType, string? correlationId);

  [LoggerMessage(2, LogLevel.Information, "Queue: start consuming messages.", EventName = "LogStartConsumingMessages")]
  public static partial void LogStartConsumingMessages (ILogger logger);

  [LoggerMessage(3, LogLevel.Information, "Queue: end consuming messages.", EventName = "LogEndConsumingMessages")]
  public static partial void LogEndConsumingMessages (ILogger logger);

  [LoggerMessage(11, LogLevel.Error, "Queue: handle error [{Error}] for message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]\n{StackTrace}", EventName = "LogHandleMessageError")]
  public static partial void LogHandleMessageError (ILogger logger, Guid? messageId, string? messageType, string? correlationId, string error, string? stackTrace);

  [LoggerMessage(12, LogLevel.Error, "Queue: handle inner error [{Error}] for message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]\n{StackTrace}", EventName = "LogHandleErrorMessageError")]
  public static partial void LogHandleInnerMessageError (ILogger logger, Guid? messageId, string? messageType, string? correlationId, string error, string? stackTrace);
}