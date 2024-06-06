
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsTests
{
  [TestMethod]
  public async Task notification__send_notification__notification_server_receive_notification()
  {
    var options = new NotificationServerOptions("localhost", 9025);
    var notifications = new List<Notification>();
    var shutdownServer = StartNotificationServer(options, notifications.Add);

    var notification = CreateNotification("a@test.com", "b@test.com", "title1", "content1", new DateTime(2024, 1, 1));
    var sendNotification = CreateNotificationSender(options);
    await sendNotification(notification, CancellationToken.None);
    shutdownServer();

    Assert.AreEqual(notifications[0], notification);
  }
}