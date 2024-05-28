
using Microsoft.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Consumer: consuming message {MessageType} error {Error}. [{TraceId}]", EventName = "LogConsumeMessageError")]
  public static partial void LogConsumeMessageError (ILogger logger, string? messageType, string? traceId, string error);

  [LoggerMessage(2, LogLevel.Error, "Dispatcher: Dispatched message {MessageType} error {Error}. [{TraceId}]", EventName = "LogDispatchedMessageError")]
  public static partial void LogDispatchedMessageError (ILogger logger, string? messageType, string? traceId, string error);

  [LoggerMessage(3, LogLevel.Information, "Handler: Handle message {MessageType}. [{TraceId}]", EventName = "LogHandleMessage")]
  public static partial void LogHandleMessage (ILogger logger, string? messageType, string? traceId);
}