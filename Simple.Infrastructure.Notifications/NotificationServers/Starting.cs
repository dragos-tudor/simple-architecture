
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static ShutdownServer StartNotificationServer(string serverName, int serverPort, IProducerConsumerCollection<Notification> notifications) =>
    StartNotificationServer(serverName, serverPort, notifications, MapMessage<Notification>);

  public static ShutdownServer StartNotificationServer<TNotification> (
    string serverName, int serverPort, IProducerConsumerCollection<TNotification> notifications, Func<MimeMessage, TNotification> mapper)
  {
    var smtpServer = CreateSmtpServer("localhost", 9025, new SmtpServerStore<TNotification>(notifications, mapper));
    _ = smtpServer.StartAsync(CancellationToken.None); // WaitingForActivation status!
    return () => smtpServer.Shutdown();
  }
}