
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  [LoggerMessage(11, LogLevel.Information, "Publisher: published message with type {MessageType}.", EventName = "LogPublishedMessage")]
  public static partial void LogPublishedMessage (ILogger logger, string messageType);
}