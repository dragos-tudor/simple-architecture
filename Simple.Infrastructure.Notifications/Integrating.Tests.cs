
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsTests
{
  [TestMethod]
  public async Task notification__send_notification__notification_received ()
  {
    var subject = Guid.NewGuid().ToString();
    var notification = (From: "x@test.com", To: "y@test.com", Subject: subject);

    var sendNotifications = CreateNotificationsSender<(string From, string To, string Subject)>(EmailServerOptions, notification => BuildMailMessage(notification.From, notification.To, notification.Subject, ""));
    var receiveNotifications = CreateNotificationsReceiver<(string From, string To, string Subject)>(EmailServerOptions, message => (GetMessageFrom(message), GetMessageTo(message), GetMessageSubject(message)));

    await sendNotifications([notification], CancellationToken.None);
    var actual = await receiveNotifications(notification.To, notification.To, (notification) => notification.Subject == subject, CancellationToken.None);

    AreEqual(actual, [notification]);
  }
}