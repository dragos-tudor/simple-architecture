
using Microsoft.Extensions.Logging;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  [LoggerMessage(3, LogLevel.Information, "Handler: Handle message {MessageType}. [{TraceId}]", EventName = "LogHandleMessage")]
  public static partial void LogHandleMessage (ILogger logger, string? messageType, string? traceId);
}