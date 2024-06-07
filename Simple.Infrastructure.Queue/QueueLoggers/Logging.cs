
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Handler: handling message {MessageType}. [{TraceId}]", EventName = "LogHandlingMessage")]
  public static partial void LogHandlingMessage (ILogger logger, string? messageType, string? traceId);

  [LoggerMessage(2, LogLevel.Information, "Consumer: finalizing message {MessageType}. [{TraceId}]", EventName = "LogFinalizingMessage")]
  public static partial void LogFinalizingMessage (ILogger logger, string? messageType, string? traceId);

  [LoggerMessage(11, LogLevel.Error, "Consumer: consuming message {MessageType} error {Error}. [{TraceId}]\n{StackTrace}", EventName = "LogConsumedMessageError")]
  public static partial void LogConsumingMessageError (ILogger logger, string? messageType, string? traceId, string error, string? stackTrace);

  [LoggerMessage(12, LogLevel.Error, "Handler: handled message {MessageType} error {Error}. [{TraceId}]", EventName = "LogHandledMessageError")]
  public static partial void LogHandledMessageError (ILogger logger, string? messageType, string? traceId, string error);


}