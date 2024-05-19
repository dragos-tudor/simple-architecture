
using Microsoft.Extensions.Logging;

namespace Simple.Architecture.Mediator;

partial class MediatorFuncs
{
  [LoggerMessage(11, LogLevel.Information, "Publisher: published message with type {MessageType}.", EventName = "LogPublishedMessage")]
  public static partial void LogPublishedMessage (ILogger logger, string messageType);
}