
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Notifications: sending mail message {Subject} to {To}.", EventName = "LogSendingMessage")]
  public static partial void LogSendingMessage (ILogger logger, string subject, string to);
}