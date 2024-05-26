
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SmtpServerOptions CreateSmtpServerOptions (string serverName, int serverPort) => new (serverName, serverPort);
}