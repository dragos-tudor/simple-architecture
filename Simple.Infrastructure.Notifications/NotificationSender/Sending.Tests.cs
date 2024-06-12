
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsTests
{
  [TestMethod]
  public async Task notification__send_notification__notification_server_receive_notification()
  {
    var options = new NotificationServerOptions("localhost", 9025);
    var notifications = new List<(string, string)>();
    var shutdownServer = StartNotificationServer(options, notifications.Add, (message) => (message.From[0].Name, message.To[0].Name) );
    var notification = ("x@test.com", "y@test.com");

    var sendNotification = CreateNotificationSender<(string, string)>(options, notification => BuildMailMessage(notification.Item1, notification.Item2, "", "", default));
    await sendNotification(notification, CancellationToken.None);
    shutdownServer();

    Assert.AreEqual(notifications[0], notification);
  }
}