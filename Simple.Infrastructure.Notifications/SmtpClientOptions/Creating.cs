
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SmtpClientOptions CreateSmtpClientOptions (string serverName, int serverPort) => new (serverName, serverPort);
}