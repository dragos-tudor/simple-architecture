
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static async Task<(SendNotifications<Notification>, ReceiveNotifications<Notification>)> IntegrateNotificationServerAsync (
    IConfiguration configuration,
    CancellationToken cancellationToken = default)
  {
    var emailServerOptions = GetConfigurationOptions<EmailServerOptions>(configuration);
    await InitializeEmailServerAsync (emailServerOptions, cancellationToken);

    return (
      CreateNotificationsSender<Notification>(emailServerOptions, MapNotification),
      CreateNotificationsReceiver(emailServerOptions, MapMessage<Notification>)
    );
  }
}
