
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Notifications: sending mail message {Subject} to {To}.", EventName = "LogSendingMessageError")]
  public static partial void LogSendingMessageError (ILogger logger, string subject, string to);
}