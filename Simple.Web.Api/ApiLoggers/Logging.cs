
using Microsoft.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Handler: handling message {MessageType}. [{TraceId}]", EventName = "LogHandlingMessage")]
  public static partial void LogHandlingMessage (ILogger logger, string? messageType, string? traceId);

  [LoggerMessage(2, LogLevel.Information, "Consumer: finalizing message {MessageType}. [{TraceId}]", EventName = "LogFinalizingMessage")]
  public static partial void LogFinalizingMessage (ILogger logger, string? messageType, string? traceId);

  [LoggerMessage(3, LogLevel.Information, "Notification: sending notification from {From} to {To} with title {Title}.", EventName = "LogSentNotification")]
  public static partial void LogSentNotification (ILogger logger, string from, string to, string title);

  [LoggerMessage(10, LogLevel.Error, "Consumer: consuming message {MessageType} error {Error}. [{TraceId}]\n{StackTrace}", EventName = "LogConsumedMessageError")]
  public static partial void LogConsumingMessageError (ILogger logger, string? messageType, string? traceId, string error, string? stackTrace);

  [LoggerMessage(11, LogLevel.Error, "Handler: handled message {MessageType} error {Error}. [{TraceId}]", EventName = "LogHandledMessageError")]
  public static partial void LogHandledMessageError (ILogger logger, string? messageType, string? traceId, string error);


}