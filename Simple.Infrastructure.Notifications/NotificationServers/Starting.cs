
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static ShutdownServer StartNotificationServer<TNotification> (
    NotificationServerOptions options, Action<TNotification> handleNotification, Func<MimeMessage, TNotification> mapper)
  {
    var smtpServer = CreateSmtpServer(options.ServerName, options.ServerPort, new SmtpServerStore<TNotification>(handleNotification, mapper));
    _ = smtpServer.StartAsync(CancellationToken.None); // WaitingForActivation status!
    return () => smtpServer.Shutdown();
  }
}