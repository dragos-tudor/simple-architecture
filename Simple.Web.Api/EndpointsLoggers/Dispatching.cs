
using Microsoft.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  [LoggerMessage(2, LogLevel.Error, "Dispatcher: Dispatched message {MessageType} error {Error}. [{TraceId}]", EventName = "LogDispatchedMessageError")]
  public static partial void LogDispatchedMessageError (ILogger logger, string? messageType, string? traceId, string error);
}