
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task SendNotificationAsync<TNotification> (TNotification notification, EmailServerOptions serverOptions, CancellationToken cancellationToken = default) where TNotification: Notification
  {
    using var client = CreateSmtpClient();
    await SendMailMessageAsync(client, MapNotification(notification), serverOptions.ContainerName, serverOptions.SmtpPort, cancellationToken);
  }
}