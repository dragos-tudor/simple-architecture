
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Consumer: handling message {MessageType}. [{correlationId}]", EventName = "LogHandlingMessage")]
  public static partial void LogHandlingMessage (ILogger logger, string? messageType, string? correlationId);

  [LoggerMessage(2, LogLevel.Information, "Consumer: finalizing message {MessageType}. [{correlationId}]", EventName = "LogFinalizingMessage")]
  public static partial void LogFinalizingMessage (ILogger logger, string? messageType, string? correlationId);

  [LoggerMessage(11, LogLevel.Error, "Consuner: consuming message {MessageType} error {Error}. [{correlationId}]\n{StackTrace}", EventName = "LogConsumedMessageError")]
  public static partial void LogConsumingMessageError (ILogger logger, string? messageType, string? correlationId, string error, string? stackTrace);

  [LoggerMessage(12, LogLevel.Error, "Consumer: handled message {MessageType} error {Error}. [{correlationId}]", EventName = "LogHandledMessageError")]
  public static partial void LogHandledMessageError (ILogger logger, string? messageType, string? correlationId, string error);
}