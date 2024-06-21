
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.EmailServer;

partial class NotificationsTests
{
  [TestMethod]
  public async Task notification__send_notification__notification_received ()
  {
    var subject = Guid.NewGuid().ToString();
    var mail = new TestMail("x@test.com", "y@test.com", subject);

    await SendMailAsync(mail, EmailServerOptions, notification => BuildMailMessage(notification.From, notification.To, notification.Subject, ""));
    var actual = await ReceiveMailsAsync<TestMail>(mail.To, mail.To, EmailServerOptions, message => new (GetMessageFrom(message), GetMessageTo(message), GetMessageSubject(message)), (mail) => mail.Subject == subject);

    AreEqual(actual, [mail]);
  }

  sealed record TestMail(string From, string To, string Subject);
}