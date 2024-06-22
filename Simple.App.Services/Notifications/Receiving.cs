

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<IEnumerable<TNotification>> ReceiveNotificationsAsync<TNotification> (string userName, string password, EmailServerOptions serverOptions, Predicate<TNotification> filterNotification, CancellationToken cancellationToken = default) where TNotification: Notification
  {
    using var client = CreateImapClient();
    var messages = await ReceiveMailMessagesAsync(client, userName, password, serverOptions.ContainerName, serverOptions.ImapPort, cancellationToken);
    return FilterMailMessages(messages, MapMessage<TNotification>, filterNotification);
  }
}