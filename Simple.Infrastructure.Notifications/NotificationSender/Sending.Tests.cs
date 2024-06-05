
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsTests
{
  [TestMethod]
  public async Task notification__send_notification__notification_server_receive_notification()
  {
    var notifications = new ConcurrentBag<Notification>();
    var shutdownServer = StartNotificationServer("localhost", 9025, notifications);

    var notification = CreateNotification("a@test.com", "b@test.com", "title1", "content1", DateTimeOffset.Now);
    var sendNotification = CreateNotificationSender("localhost", 9025);
    await sendNotification(notification, CancellationToken.None);
    shutdownServer();

    var actual = FindNotificationByTitle(notifications, notification.Title);
    StringAssert.Contains(actual.First().Content, "content1", StringComparison.InvariantCulture);
  }
}