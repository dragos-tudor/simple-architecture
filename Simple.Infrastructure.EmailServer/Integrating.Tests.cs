
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.EmailServer;

partial class NotificationsTests
{
  [TestMethod]
  public async Task notification__send_notification__notification_received ()
  {
    using var smtpClient = CreateSmtpClient();
    using var imapClient = CreateImapClient();
    var mail = new TestMail("x@test.com", "y@test.com", Guid.NewGuid().ToString());
    using var message = BuildMailMessage(mail.From, mail.To, mail.Subject, "");

    await SendMailMessageAsync(smtpClient, message, EmailServerOptions.ContainerName, EmailServerOptions.SmtpPort);
    var actual = await ReceiveMailMessagesAsync(imapClient, mail.To, mail.To, EmailServerOptions.ContainerName, EmailServerOptions.ImapPort);

    var mapMessage = (MimeMessage message) => new TestMail(GetMessageFrom(message), GetMessageTo(message), GetMessageSubject(message));
    var filterMail = (TestMail receivedMail) => receivedMail.Subject == mail.Subject;
    AreEqual(actual.Select(mapMessage).Where(filterMail), [mail]);
  }

  sealed record TestMail(string From, string To, string Subject);
}