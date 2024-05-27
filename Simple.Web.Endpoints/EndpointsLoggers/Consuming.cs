
using Microsoft.Extensions.Logging;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Consumer: consuming message {MessageType} error {Error}. [{TraceId}]", EventName = "LogConsumeMessageError")]
  public static partial void LogConsumeMessageError (ILogger logger, string? messageType, string? traceId, string error);
}