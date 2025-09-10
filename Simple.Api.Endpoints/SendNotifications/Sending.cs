
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task SendNotificationAsync<TNotification>(TNotification notification, string mailServerName, int smtpPort, CancellationToken cancellationToken = default) where TNotification : INotification
  {
    using var client = CreateSmtpClient();
    using var mailMessage = MapNotification(notification);

    await SendMailMessageAsync(client, mailMessage, mailServerName, smtpPort, cancellationToken);
  }
}