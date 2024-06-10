
namespace Simple.Web.Api;

partial class ApiFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Notification: sending notification from {From} to {To} with title {Title}.", EventName = "LogSentNotification")]
  public static partial void LogSentNotification (ILogger logger, string from, string to, string title);

  [LoggerMessage(2, LogLevel.Information, "App: shuting down app.", EventName = "LogShutingDownApp")]
  public static partial void LogShutingDownApp(ILogger logger);
}