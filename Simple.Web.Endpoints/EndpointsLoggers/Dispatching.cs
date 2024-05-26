
using Microsoft.Extensions.Logging;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Dispatcher: dispatched message {MessageType} error {Error}.", EventName = "LogDispatchMessageError")]
  public static partial void LogDispatchMessageError (ILogger logger, string messageType, string error);
}