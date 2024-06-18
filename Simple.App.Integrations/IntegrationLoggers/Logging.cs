
namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Notification: sending notification from {From} to {To} with title {Title}.", EventName = "LogSentNotification")]
  public static partial void LogSentNotification (ILogger logger, string from, string to, string title);
}