
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.MessageQueue;

partial class MessageQueueFuncs
{
  [LoggerMessage(1, LogLevel.Information, "MessageQueue: start process messages.", EventName = "LogStartProcessMessages")]
  public static partial void LogStartProcessMessages(ILogger logger);

  [LoggerMessage(2, LogLevel.Information, "MessageQueue: end process messages.", EventName = "LogEndProcessMessages")]
  public static partial void LogEndProcessMessages(ILogger logger);

  [LoggerMessage(3, LogLevel.Information, "MessageQueue: process message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]", EventName = "LogProcessMessage")]
  public static partial void LogProcessMessage(ILogger logger, Guid? messageId, string? messageType, string? correlationId);

  [LoggerMessage(11, LogLevel.Error, "MessageQueue: process error [{Error}] for message with id [{MessageId}] and type [{MessageType}]. [{correlationId}]\n{StackTrace}", EventName = "LogProcessMessageError")]
  public static partial void LogProcessMessageError(ILogger logger, Guid? messageId, string? messageType, string? correlationId, string error, string? stackTrace);
}